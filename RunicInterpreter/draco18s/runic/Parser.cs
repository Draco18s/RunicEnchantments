using RunicInterpreter.draco18s.runic;
using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.runic.runes;
using RunicInterpreter.draco18s.math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RunicInterpreter.draco18s.runic {
	public static class Parser {

		public static ParseError Parse(string v, out ExecutionContext context) {
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
				string s2 = s;
				for(int x = s2.Length - 1; x >= 0; x--) {
					char cat = s2[x];
					UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(cat);
					if(uc == UnicodeCategory.NonSpacingMark || uc == UnicodeCategory.EnclosingMark || uc == UnicodeCategory.OtherNotAssigned) {
						s2.Remove(x, 1);
					}
				}
				if(s2.Length > max) {
					max = s.Length;
				}
			}
			//char[,] runesCodes = new char[max,lines.Length];
			if(lines.Length == 1 && !(code.Contains("$") || code.Contains("@"))) {
				lines[0] = lines[0] + "@";
				max++;
			}
			IExecutableRune[,] runes = new IExecutableRune[max, lines.Length];
			char[,] modifiers = new char[max, lines.Length];
			for(int y = 0; y < lines.Length; y++) {
				int mutateOffset = 0;
				for(int x = 0; x < max; x++) {
					char cat = (x + mutateOffset < lines[y].Length ? lines[y][x + mutateOffset] : ' ');
					if(cat == '\r') cat = ' ';
					IExecutableRune r = RuneRegistry.GetRune(cat);
					modifiers[x, y] = ' ';
					if(r == null) {
						runes[x, y] = new RuneCharLiteral(cat);
					}
					else {
						runes[x, y] = r;
						if(r is RuneEntrySimple /*&& (x == 0 || y == 0 || x == max - 1 || y == lines.Length - 1)*/) {
							entries.Add(new Vector2Int(x, y));
						}
					}
					int o = 1;
					while(x + mutateOffset + o < lines[y].Length) {
						char mod = lines[y][x + mutateOffset + o];
						UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(mod);
						if(uc == UnicodeCategory.NonSpacingMark || uc == UnicodeCategory.EnclosingMark || uc == UnicodeCategory.OtherNotAssigned) {
							if(modifiers[x, y] != ' ') {
								context = null;
								return new ParseError(ParseErrorType.TOO_MANY_MODIFIERS, new Vector2Int(x, y), mod);
							}
							IExecutableRune rr = runes[x, y];
							if(rr is RuneReflector || rr is RuneReflectAll || rr is RuneDiagonalReflector || rr is RuneDirection) {
								context = null;
								return new ParseError(ParseErrorType.INVALID_MODIFIER, new Vector2Int(x, y), mod);
							}
							modifiers[x, y] = mod;
							mutateOffset++;
						}
						else {
							break;
						}
						o++;
					}
				}
			}
			if(entries.Count == 0) {
				if(lines.Length == 1) {
					entries.Add(new Vector2Int(-1, 0));
				}
				else {
					context = null;
					return new ParseError(ParseErrorType.NO_ENTRY, Vector2Int.zero, '>');
				}
			}
			context = new ExecutionContext(runes, entries, modifiers).SetReader(DefaultReader).SetWriter(DefaultWriter);
			return new ParseError(ParseErrorType.NONE, Vector2Int.zero, ' ');
		}

		public static object DefaultReader() {
			StringBuilder sb = new StringBuilder();
			bool forceread = false;
			while(Console.In.Peek() != -1) {
				char c = (char)Console.In.Read();
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
				return d;
			}
			else if(sb.Length > 1) {
				return s;
			}
			else if(sb.Length > 0) {
				return s[0];
			}
			return null;
		}

		public static object DefaultWriter(object o) {
			Console.Out.Write(o);
			return o;
		}
	}
}