using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneDelay : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.SetSkip(1);
			return false;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('y', this);
			return this;
		}
	}
}