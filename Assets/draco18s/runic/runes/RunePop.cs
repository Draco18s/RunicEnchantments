using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RunePop : IExecutableRune {
		public bool Execute(Pointer pointer, GameObject go) {
			pointer.Pop();
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('~', this);
			return this;
		}
	}
}