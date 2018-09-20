using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneMinMana : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				ValueType v = (ValueType)o;
				if(MathHelper.Compare(pointer.GetMana(), v) >= 0) {
					return true;
				}
				else {
					pointer.Push(o);
				}
			}
			return false;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('M', this);
			return this;
		}
	}
}