using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneReadNumber : IExecutableRune {
		char c;
		public RuneReadNumber(char c) {
			this.c = c;
		}
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.SetReadType(Pointer.ReadType.READ_NUM);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c, this);
			return this;
		}
	}
}