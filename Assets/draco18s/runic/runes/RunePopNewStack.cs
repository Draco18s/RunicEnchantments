using UnityEngine;
using System.Collections;
using Assets.draco18s.runic.init;
using System;
using Assets.draco18s.util;

namespace Assets.draco18s.runic.runes {
	public class RunePopNewStack : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				ValueType v = (ValueType)o;
				int d = (int)MathHelper.GetValue(v);
				pointer.PopNewStack(d);
				if(d > 0)
					pointer.DeductMana(1);
			}
			else {
				pointer.Push(o);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('[', this);
			return this;
		}
	}
}