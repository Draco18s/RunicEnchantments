using System.Collections;
using RunicInterpreter.draco18s.runic.init;
using System;
using RunicInterpreter.draco18s.util;

namespace RunicInterpreter.draco18s.runic.runes {
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