using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneDirection : IExecutableRune {
		Direction dir;
		char c;
		public RuneDirection(Direction dir, char c) {
			this.dir = dir;
			this.c = c;
		}
		public bool Execute(Pointer pointer, GameObject go) {
			pointer.direction = dir;
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c, this);
			return this;
		}
	}
}