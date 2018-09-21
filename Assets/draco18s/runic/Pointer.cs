using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.draco18s.util;

namespace Assets.draco18s.runic {
	public class Pointer {
		protected int age = 0;
		protected int mana;
		protected bool skip = false;
		protected ReadType readType = ReadType.EXECUTE;

		protected List<object> stack;

		public Vector2Int position;
		public Direction direction;

		public Pointer(int m, Direction d, Vector2Int pos) {
			mana = m;
			stack = new List<object>();
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

		public void Execute() {
			age++;
		}

		public bool isSkipping() {
			bool r = skip;
			skip = false;
			return r;
		}

		public void SetSkip() {
			skip = true;
		}

		public void SetReadType(ReadType t) {
			readType = t;
		}

		public string PrintStack() {
			int num = 0;
			string s = "";
			for(int i = stack.Count- 1; i >=0 && num < 5; i--) {
				s += stack[i].ToString() + "\n";
				num++;
			}
			return s;
		}

		public ReadType GetReadType() {
			return readType;
		}

		public enum ReadType {
			READ_CHAR, READ_CHAR_CONTINUOUS, READ_STR,EXECUTE
		}
	}
}