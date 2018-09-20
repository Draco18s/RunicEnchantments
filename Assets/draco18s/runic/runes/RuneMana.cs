using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneMana : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.Push(pointer.GetMana());
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('m', this);
			return this;
		}
	}
}