using Assets.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneVec3 : IExecutableRune {
		public bool Execute(Pointer pointer, GameObject go) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			object c = pointer.Pop();
			if(a is ValueType && b is ValueType && c is ValueType) {
				float x = (float)Convert.ToDouble(a);
				float y = (float)Convert.ToDouble(b);
				float z = (float)Convert.ToDouble(c);
				pointer.Push(new Vector3(x, y, z));
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('V', this);
			return this;
		}
	}
}