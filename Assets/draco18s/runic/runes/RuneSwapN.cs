using Assets.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.draco18s.util;

namespace Assets.draco18s.runic.runes {
	public class RuneSwapN : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				int n = (int)Convert.ToDouble(o);
				if(Math.Abs(n) <= 2) return true; //rotating 0 or 1 items will have no effect
				char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
				if(modifier == '̹') {
					pointer.SwapNStacksStack(n);
				}
				else {
					bool right = n > 0;
					if(n < 0) n *= -1;
					if(pointer.GetStackSize() < n) n = pointer.GetStackSize();
					List<object> list = new List<object>();
					for(int i = 0; i < n; i++) {
						list.Add(pointer.Pop());
					}
					list.Reverse();
					if(right)
						list.RotateListRight();
					else
						list.RotateListLeft();
					for(int i = 0; i < n; i++) {
						pointer.Push(list[i]);
					}
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('s', this);
			return this;
		}
	}
}