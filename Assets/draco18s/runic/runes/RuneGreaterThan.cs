using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneGreaterThan : IExecutableRune {
		public bool Execute(Pointer pointer, GameObject go) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a is ValueType && b is ValueType) {
				MathHelper.NumericRelationship r = MathHelper.Compare((ValueType)b, (ValueType)a);
				pointer.Push(r == MathHelper.NumericRelationship.GreaterThan ? 1 : 0);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(')', this);
			return this;
		}
	}
}