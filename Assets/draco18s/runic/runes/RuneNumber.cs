using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	internal class RuneNumber : IExecutableRune {
		public int value;

		public RuneNumber(int v) {
			this.value = v;
		}

		public bool Execute(Pointer pointer, GameObject go) {
			pointer.Push(value);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(value.ToString()[0],this);
			return this;
		}
	}
}