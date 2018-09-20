using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneToValue : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				pointer.Push(MathHelper.GetValue((ValueType)o));
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('n', this);
			return this;
		}
	}
}