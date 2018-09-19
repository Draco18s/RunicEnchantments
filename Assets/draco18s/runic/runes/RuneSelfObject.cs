using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneSelfObject : IExecutableRune {
		public bool Execute(Pointer pointer, GameObject go) {
			pointer.Push(go);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('t', this);
			return this;
		}
	}
}