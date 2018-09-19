using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	internal class RuneTerminator : IExecutableRune {
		public bool Execute(Pointer pointer, GameObject go) {
			pointer.DeductMana(pointer.GetMana());
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(';',this);
			return this;
		}
	}
}