using Assets.draco18s.runic.init;
using Assets.draco18s.runic.runes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic {
	public class Parser {
		public string code;
		public IExecutableRune[,] runes;
		public List<Pointer> pointers;
		List<Vector2Int> entries;
		private GameObject thisGo;

		public Parser(GameObject self) {
			thisGo = self;
		}

		public ParseError Parse(string v) {
			entries = new List<Vector2Int>();
			pointers = new List<Pointer>();
			code = v;
			code.Replace("\r", String.Empty);
			string[] lines = code.Split('\n');
			int max = 0;
			foreach(string s in lines) {
				if(s.Length > max) {
					max = s.Length;
				}
			}
			//char[,] runesCodes = new char[max,lines.Length];
			runes = new IExecutableRune[max, lines.Length];
			for(int y = 0; y < lines.Length; y++) {
				for(int x = 0; x < max; x++) {
					char cat = (x < lines[y].Length ? lines[y][x] : ' ');
					if(cat == '\r') cat = ' ';
					IExecutableRune r = RuneRegistry.GetRune(cat);
					if(r == null) {
						return new ParseError(ParseErrorType.INVALID_CHARACTER, new Vector2Int(x,y), cat);
					}
					runes[x, y] = r;
					if(r is RuneEntrySimple /*&& (x == 0 || y == 0 || x == max - 1 || y == lines.Length - 1)*/) {
						entries.Add(new Vector2Int(x, y));
					}
				}
			}
			if(entries.Count == 0) return new ParseError(ParseErrorType.NO_ENTRY, Vector2Int.zero, '>');
			return new ParseError(ParseErrorType.NONE, Vector2Int.zero, ' ');
		}

		public void SpawnPointer() {
			foreach(Vector2Int v in entries) {
				Pointer pointer = new Pointer(0, Direction.RIGHT, v);
				if(runes[v.x, v.y].Execute(pointer, thisGo)) {
					pointer.position.x += DirectionHelper.GetX(pointer.direction);
					pointer.position.y += DirectionHelper.GetY(pointer.direction);
				}
				pointers.Add(pointer);
			}
		}

		public bool Tick() {
			foreach(Pointer pointer in pointers) {
				pointer.Execute();
				if(pointer.isSkipping() || runes[pointer.position.x, pointer.position.y].Execute(pointer, thisGo)) {
					pointer.position.x += DirectionHelper.GetX(pointer.direction);
					pointer.position.y += DirectionHelper.GetY(pointer.direction);
					int width = runes.GetLength(0);
					int height = runes.GetLength(1);
					if(pointer.position.x >= width) {
						pointer.position.x = 0;
					}
					if(pointer.position.y >= height) {
						pointer.position.y = 0;
					}
					if(pointer.position.x < 0) {
						pointer.position.x = width-1;
					}
					if(pointer.position.y < 0) {
						pointer.position.y = height-1;
					}
				}
			}
			pointers.ForEach(x => {
				int xi = pointers.IndexOf(x);
				pointers.ForEach(y => {
					int yi = pointers.IndexOf(y);
					if(xi < yi && x.position == y.position && x.direction == y.direction) {
						x.Merge(y);
						y.DeductMana(y.GetMana());
					}
				});
			});
			pointers.RemoveAll(x => x.GetMana() <= 0);
			return pointers.Count > 0;
		}
	}
}