using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Assets.draco18s.config {
	public static class Localization {
		private static Dictionary<string, string> entries;
		private static Dictionary<string, string> enUS;

		private static bool hasInitialized = false;

		public static void initialize() {
			if(hasInitialized) return;
			hasInitialized = true;
			entries = new Dictionary<string, string>();
			enUS = new Dictionary<string, string>();

			TextAsset file = Resources.Load<TextAsset>("lang/en_US");
			string fs = file.text;
			string[] fLines = Regex.Split ( fs, "\n|\r|\r\n" );
			string entry;
			for(int i = 0; i < fLines.Length; i++) {
				entry = fLines[i];
				if(entry.StartsWith("#")) continue;
				string[] parts = entry.Split('=');
				if(parts.Length == 2) {
					entries.Add(parts[0], parts[1]);
				}
				if(parts.Length > 2) {
					entries.Add(parts[0], entry.Substring(parts[0].Length + 1));
				}
			}
			file = Resources.Load<TextAsset>("lang/en_US");
			fs = file.text;
			fLines = Regex.Split(fs, "\n|\r|\r\n");
			for(int i = 0; i < fLines.Length; i++) {
				entry = fLines[i];
				if(entry.StartsWith("#")) continue;
				string[] parts = entry.Split('=');
				if(parts.Length == 2) {
					enUS.Add(parts[0], parts[1]);
				}
				if(parts.Length > 2) {
					enUS.Add(parts[0], entry.Substring(parts[0].Length+1));
				}
			}
		}

		public static string translateToLocal(string input, params object[] args) {
			return string.Format(translateToLocal(input), args);
		}

		public static string translateToLocal(string input) {
			if(input.Length <= 1) return input;
			string v;
			string[] s = input.Split('.');
			if(!hasInitialized) {
				if(s.Length >= 3) {
					v = s[s.Length - 2];
					if(v.Length <= 1) return v;
					List<string> vv = new List<string>();
					for(int i = 1; i < s.Length - 1; i++) {
						vv.Add(s[i]);
					}
					s = vv.ToArray();
					v = string.Join(".", s);
					return v;
				}
				else {
					return input;
				}
			}
			entries.TryGetValue(input, out v);
			if(v == null || v.Equals("")) {
				enUS.TryGetValue(input, out v);
				if(v == null || v.Equals("")) {
					if(s.Length >= 3) {
						v = s[s.Length - 2];
						if(v.Length <= 1) return v;
						List<string> vv = new List<string>();
						for(int i = 1; i < s.Length - 1; i++) {
							vv.Add(s[i]);
						}
						s = vv.ToArray();
						v = string.Join(".", s);
					}
					else {
						v = s.Length > 1 ? s[1] : s[0];
					}
					Debug.Log("Missing Localization entry for '" + input + "' for current language and en_US");
				}
				else {
					Debug.Log("Missing Localization entry for '" + input + "' for current language");
				}
			}
			v = ParseForControlSequences(v);
			return v;
		}

		private static string ParseForControlSequences(string v) {
			string pattern = "\\[([A-z_\\-]+),(\\d+)\\]";
			Regex reg = new Regex(pattern);
			v = Regex.Replace(v, pattern, "<size=$2><sprite name=$1></size>");

			pattern = "\\[\\\\n\\]";
			reg = new Regex(pattern);
			v = Regex.Replace(v, pattern, "\n");
			return v;
		}

		public static void updateLanguage(string lang) {
			entries.Clear();
			TextAsset file = Resources.Load<TextAsset>("lang/en_US.lang");
			string fs = file.text;
			string[] fLines = Regex.Split(fs, "\n|\r|\r\n");
			string entry;
			for(int i = 0; i < fLines.Length; i++) {
				entry = fLines[i];
				if(entry.StartsWith("#")) continue;
				string[] parts = entry.Split('=');
				if(parts.Length == 2) {
					entries.Add(parts[0], parts[1]);
				}
			}
		}

		public static void addLanguageEntry(string unlocal, string local) {
			if(entries.ContainsKey(unlocal)) {
				entries.Remove(unlocal);
			}
			entries.Add(unlocal, local);
		}

		public static void addFallbackEntry(string unlocal, string local) {
			if(enUS.ContainsKey(unlocal)) {
				enUS.Remove(unlocal);
			}
			enUS.Add(unlocal, local);
		}
	}
}