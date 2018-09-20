using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneSelfObject : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.Push(context.self);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('t', this);
			return this;
		}
	}
}