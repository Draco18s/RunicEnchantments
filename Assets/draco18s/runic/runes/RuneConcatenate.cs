using Assets.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneConcatenate : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			string x = "";
			string y = "";
			if((a is ValueType || a is string))
				x = a.ToString();
			if((b is ValueType || b is string))
				y = b.ToString();
			pointer.Push(y + x);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('q', this);
			return this;
		}
	}
}