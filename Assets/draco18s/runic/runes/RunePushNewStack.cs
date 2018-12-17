using UnityEngine;
using System.Collections;
using Assets.draco18s.runic.init;
using System;
using Assets.draco18s.util;

namespace Assets.draco18s.runic.runes {
	public class RunePushNewStack : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.PushNewStack();
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(']', this);
			return this;
		}
	}
}