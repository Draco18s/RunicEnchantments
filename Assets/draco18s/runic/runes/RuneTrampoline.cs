using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneTrampoline : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.SetSkip(1);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('!', this);
			return this;
		}
	}
}