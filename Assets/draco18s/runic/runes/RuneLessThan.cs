using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneLessThan : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a is ValueType && b is ValueType) {
				MathHelper.NumericRelationship q = MathHelper.Compare((ValueType)b, (ValueType)a);
				bool r = q == MathHelper.NumericRelationship.LessThan;
				/*char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
				if(modifier == '̸') {
					r = !r;
				}*/
				pointer.Push(r);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('(', this);
			return this;
		}
	}
}