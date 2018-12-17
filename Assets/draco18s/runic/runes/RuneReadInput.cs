using Assets.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.draco18s.runic.runes {
	public class RuneReadInput : IExecutableRune {
		private string inputStr = "";
		public static int inputInd = -1;
		public bool Execute(Pointer pointer, ExecutionContext context) {
			if(inputInd == -1) {
				InputField inputField = GameObject.Find("Canvas/InputField").GetComponent<InputField>();
				if(inputField == null) return true;

				inputStr = inputField.text;
				inputInd = 0;
			}
			StringBuilder sb = new StringBuilder();
			bool forceread = false;
			while(inputInd < inputStr.Length) {
				char c = Read();
				if(c == '\r') continue;
				if(!forceread && char.IsWhiteSpace(c)) break;
				if(!forceread && c == '\\') {
					forceread = true;
				}
				else {
					forceread = false;
					sb.Append(c);
				}
			}

			double d;
			string s = sb.ToString();
			if(double.TryParse(s, out d)) {
				pointer.Push(d);
			}
			else if(sb.Length > 1) {
				pointer.Push(s);
			}
			else if(sb.Length > 0) {
				pointer.Push(s[0]);
			}
			return true;
		}

		private char Read() {
			char c = inputStr[inputInd];
			inputInd++;
			return c;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('i', this);
			return this;
		}
	}
}