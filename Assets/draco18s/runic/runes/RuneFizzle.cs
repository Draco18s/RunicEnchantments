using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneFizzle : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.DeductMana(1);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('F', this);
			return this;
		}
	}
}