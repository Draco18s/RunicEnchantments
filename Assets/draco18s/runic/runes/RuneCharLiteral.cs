using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneCharLiteral : IExecutableRune {
		public char c;
		public RuneCharLiteral(char c) {
			this.c = c;
		}
		public bool Execute(Pointer pointer, ExecutionContext context) {
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c, this);
			return this;
		}
	}
}