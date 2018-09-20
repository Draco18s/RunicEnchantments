using Assets.draco18s.generpg.init;
using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneGetObject : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is string) {
				GameObject go = ObjectRegistry.GetObject((string)o);
				pointer.Push(go);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('G', this);
			return this;
		}
	}
}