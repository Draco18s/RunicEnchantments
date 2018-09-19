using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneReverse : IExecutableRune {
		public bool Execute(Pointer pointer, GameObject go) {
			pointer.ReverseStack();
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('r', this);
			return this;
		}
	}
}