using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneConditional : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			if(a is ValueType) {
				MathHelper.NumericRelationship r = MathHelper.Compare((ValueType)a, 0);
				if(r != MathHelper.NumericRelationship.EqualTo) pointer.SetSkip();
			}
			else {
				if(a == null) pointer.SetSkip();
				else pointer.Push(a);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('?', this);
			return this;
		}
	}
}