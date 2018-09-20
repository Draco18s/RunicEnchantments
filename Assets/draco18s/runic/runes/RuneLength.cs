using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneLength : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.Push(pointer.GetStackSize());
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('l', this);
			return this;
		}
	}
}