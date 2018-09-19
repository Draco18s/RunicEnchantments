using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneObjectPosition : IExecutableRune {
		public bool Execute(Pointer pointer, GameObject go) {
			object o = pointer.Pop();
			if(o is GameObject) {
				pointer.Push(((GameObject)o).transform.position);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('P', this);
			return this;
		}
	}
}