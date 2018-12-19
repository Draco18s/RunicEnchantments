using System;
using System.Collections.Generic;
using RunicInterpreter.draco18s.util;
using RunicInterpreter.draco18s.math;

namespace RunicInterpreter.draco18s.runic {
	public class Pointer {
		protected int age = 0;
		protected int mana;
		protected int skip = 0;
		protected int delay = 0;
		protected ReadType readType = ReadType.EXECUTE;

		protected List<object> stack;
		protected List<List<object>> substacks;

		public Vector2Int position;
		public Direction direction;

		public Pointer(int m, Direction d, Vector2Int pos) {
			mana = m;
			stack = new List<object>();
			substacks = new List<List<object>>();
			direction = d;
			position = pos;
		}

		public void RotateStack(bool rotLeft) {
			if(rotLeft) stack.RotateListLeft();
			else stack.RotateListRight();
		}

		public int GetAge() {
			return age;
		}

		public int GetMana() {
			return mana;
		}

		public void DeductMana(int amt) {
			mana -= amt;
		}

		public void Push(object o) {
			stack.Add(o);
		}

		public object Pop() {
			if(stack.Count > 0) {
				object o = stack[stack.Count - 1];
				stack.RemoveAt(stack.Count - 1);
				return o;
			}
			mana = 0;
			return null;
		}

		public void ReverseStack() {
			stack.Reverse();
		}

		public int GetStackSize() {
			return stack.Count;
		}

		public void Merge(Pointer newer) {
			mana += newer.GetMana();
			//keep stack?
		}

		public void PopNewStack(int size) {
			List<object> newStack = new List<object>();
			while(size-- > 0) {
				object o = Pop();
				newStack.Add(o);
			}
			newStack.Reverse();
			substacks.Add(stack);
			stack = newStack;
			DeductMana(1);
		}

		public void PushNewStack() {
			if(substacks.Count == 0) {
				while(GetStackSize() > 0) {
					Pop();
				}
				return;
			}
			//stack.Reverse();
			List<object> oldStack = stack;
			int last = substacks.Count - 1;
			List<object> newStack = substacks[last];
			substacks.RemoveAt(last);
			stack = newStack;
			while(oldStack.Count > 0) {
				Push(oldStack[0]);
				oldStack.RemoveAt(0);
			}
		}

		public void Execute() {
			age++;
			delay = Math.Max(delay-1,0);
		}

		public bool isSkipping(bool reduce) {
			bool r = skip > 0;
			if(reduce)
				skip = Math.Max(skip - 1, 0);
			return r;
		}

		public int GetDelayAmt() {
			return delay;
		}

		public void SetSkip(int amt) {
			skip += amt;
		}

		public void SetDelay(int amt) {
			delay += amt;
		}

		public void SetReadType(ReadType t) {
			readType = t;
		}

		public ReadType GetReadType() {
			return readType;
		}

		public enum ReadType {
			READ_CHAR, READ_CHAR_CONTINUOUS, READ_STR,EXECUTE
		}
	}
}