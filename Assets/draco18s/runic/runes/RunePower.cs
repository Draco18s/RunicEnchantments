using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RunePower : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a is ValueType && b is ValueType) {
				if(a is Vector3 || b is Vector3) {
				}
				else if(a is char || b is char) {
					double x = MathHelper.GetValue((ValueType)a);
					double y = MathHelper.GetValue((ValueType)b);
					pointer.Push(Math.Pow(y, x));
				}
				else {
					double x = Convert.ToDouble(a);
					double y = Convert.ToDouble(b);
					pointer.Push(Math.Pow(y, x));
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('p', this);
			return this;
		}
	}
}