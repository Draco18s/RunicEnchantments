using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneToChar : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				ValueType v = (ValueType)o;
				int i = (int)MathHelper.GetValue(v);
				char c = (char)i;
				pointer.Push(c);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('k', this);
			return this;
		}
	}
}