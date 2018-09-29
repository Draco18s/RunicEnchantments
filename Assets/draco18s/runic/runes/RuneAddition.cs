﻿using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	internal class RuneAddition : IExecutableRune {

		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a != null && b != null) {
				if(a is ValueType && b is ValueType) {
					if(MathHelper.IsInteger((ValueType)a) && MathHelper.IsInteger((ValueType)b)) {
						int c = (int)a + (int)b;
						pointer.Push(c);
					}
					else if(a is Vector3 && b is Vector3) {
						pointer.Push(((Vector3)a + (Vector3)b));
					}
					else {
						double c = MathHelper.GetValue((ValueType)a) + MathHelper.GetValue((ValueType)b);
						pointer.Push(c);
					}
				}
				else if(b is string || a is string) {
					string s1 = a.ToString();
					string s2 = b.ToString();
					
					pointer.Push(s1+s2);
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('+',this);
			return this;
		}
	}
}