using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneInstantiate : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			if(pointer.GetMana() < 10) return false;
			object v = pointer.Pop();
			object o = pointer.Pop();
			if(o is GameObject && v is Vector3) {
				pointer.Push(Object.Instantiate((GameObject)o, (Vector3)v, Quaternion.identity));
				pointer.DeductMana(10);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('O', this);
			return this;
		}
	}
}