using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneReflectAll : IExecutableRune {
		public bool Execute(Pointer pointer, GameObject go) {
			pointer.direction = DirectionHelper.Reflect(pointer.direction);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('#', this);
			return this;
		}
	}
}