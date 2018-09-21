using Assets.draco18s.runic.init;
using Assets.draco18s.runic.runes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic {
	public static class Parser {
		/*public static string[] Validate(string v) {
			return v.Split('`');
		}*/

		public static ParseError Parse(string v, GameObject attatchedGameObj, out ExecutionContext context) {
			List<Vector2Int> entries = new List<Vector2Int>();
			string code = v;
			if(!code.Contains(";") && !code.Contains("F") && !code.Contains("@")) {
				context = null;
				return new ParseError(ParseErrorType.NO_TERMINATOR, Vector2Int.zero, ';');
			}
			code.Replace("\r", String.Empty);
			string[] lines = code.Split('\n');
			int max = 0;
			foreach(string s in lines) {
				if(s.Length > max) {
					max = s.Length;
				}
			}
			//char[,] runesCodes = new char[max,lines.Length];
			if(lines.Length == 1 && !(code.Contains("$") || code.Contains("@"))) {
				lines[0] = lines[0] + "@";
				max++;
			}
			IExecutableRune[,] runes = new IExecutableRune[max, lines.Length];
			for(int y = 0; y < lines.Length; y++) {
				for(int x = 0; x < max; x++) {
					char cat = (x < lines[y].Length ? lines[y][x] : ' ');
					if(cat == '\r') cat = ' ';
					IExecutableRune r = RuneRegistry.GetRune(cat);
					if(r == null) {
						//context = null;
						//return new ParseError(ParseErrorType.INVALID_CHARACTER, new Vector2Int(x,y), cat);
						runes[x, y] = new RuneCharLiteral(cat);// RuneRegistry.GetRune(' ');
					}
					else {
						runes[x, y] = r;
						if(r is RuneEntrySimple /*&& (x == 0 || y == 0 || x == max - 1 || y == lines.Length - 1)*/) {
							entries.Add(new Vector2Int(x, y));
						}
					}
				}
			}
			if(entries.Count == 0) {
				if(lines.Length == 1) {
					/*IExecutableRune[,] runesb = new IExecutableRune[max + 1, lines.Length];
					runesb[0, 0] = RuneRegistry.GetRune('>');
					entries.Add(new Vector2Int(0, 0));
					for(int i = 0; i < max; i++) {
						runesb[i + 1, 0] = runes[i, 0];
					}
					runes = runesb;*/
					entries.Add(new Vector2Int(-1, 0));
				}
				else {
					context = null;
					return new ParseError(ParseErrorType.NO_ENTRY, Vector2Int.zero, '>');
				}
			}
			context = new ExecutionContext(runes, entries, attatchedGameObj);
			return new ParseError(ParseErrorType.NONE, Vector2Int.zero, ' ');
		}
	}
}