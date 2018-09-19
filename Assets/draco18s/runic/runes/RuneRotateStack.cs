using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneRotateStack : IExecutableRune {
		bool rotLeft;
		char c;
		public RuneRotateStack(bool left, char c) {
			rotLeft = left;
			this.c = c;
		}

		public bool Execute(Pointer pointer, GameObject go) {
			pointer.RotateStack(rotLeft);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c, this);
			return this;
		}
	}
}