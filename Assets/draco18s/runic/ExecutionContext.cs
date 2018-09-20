using Assets.draco18s.runic.init;
using Assets.draco18s.runic.runes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic {
	public class ExecutionContext {
		public readonly GameObject self;

		private IExecutableRune[,] runes;
		private List<Vector2Int> entries;
		private List<Pointer> newpointers;
		private List<Pointer> pointers;

		public ExecutionContext(IExecutableRune[,] runes, List<Vector2Int> entryPoints, GameObject selfGO) {
			this.runes = runes;
			this.entries = entryPoints;
			self = selfGO;
		}

		public void Initialize() {
			pointers = new List<Pointer>();
			newpointers = new List<Pointer>();
			foreach(Vector2Int v in entries) {
				Pointer pointer = new Pointer(0, Direction.RIGHT, v);
				if(runes[v.x, v.y].Execute(pointer, null)) {
					pointer.position.x += DirectionHelper.GetX(pointer.direction);
					pointer.position.y += DirectionHelper.GetY(pointer.direction);
				}
				pointers.Add(pointer);
			}
		}

		public void SpawnPointer(Pointer p) {
			newpointers.Add(p);
			AdvancePointer(p);
		}

		public bool Tick() {
			foreach(Pointer pointer in pointers) {
				pointer.Execute();
				if(pointer.GetReadType() == Pointer.ReadType.EXECUTE) {
					if(pointer.isSkipping() || runes[pointer.position.x, pointer.position.y].Execute(pointer, this)) {
						AdvancePointer(pointer);
					}
				}
				else if(pointer.GetReadType() == Pointer.ReadType.READ_CHAR) {
					char c = RuneRegistry.GetRuneChar(runes[pointer.position.x, pointer.position.y]);
					pointer.Push(c);
					pointer.SetReadType(Pointer.ReadType.EXECUTE);
					AdvancePointer(pointer);
				}
				else if(pointer.GetReadType() == Pointer.ReadType.READ_CHAR_CONTINUOUS) {
					char c = RuneRegistry.GetRuneChar(runes[pointer.position.x, pointer.position.y]);
					if(c.Equals('`')) {
						pointer.SetReadType(Pointer.ReadType.EXECUTE);
					}
					else {
						pointer.Push(c);
					}
					AdvancePointer(pointer);
				}
				else if(pointer.GetReadType() == Pointer.ReadType.READ_STR) {
					char c = RuneRegistry.GetRuneChar(runes[pointer.position.x, pointer.position.y]);
					if(c.Equals('\"')) {
						pointer.SetReadType(Pointer.ReadType.EXECUTE);
					}
					else {
						object o = pointer.GetStackSize() > 0 ? pointer.Pop() : null;
						if(o is string || o == null) {
							pointer.Push((string)o + c);
						}
						else {
							pointer.Push(o);
							pointer.Push(c.ToString());
						}
					}
					AdvancePointer(pointer);
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
			pointers.AddRange(newpointers);
			newpointers.Clear();
			pointers.RemoveAll(x => x.GetMana() <= 0);
			return pointers.Count > 0;
		}

		private void AdvancePointer(Pointer pointer) {
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
				pointer.position.x = width - 1;
			}
			if(pointer.position.y < 0) {
				pointer.position.y = height - 1;
			}
		}
	}
}