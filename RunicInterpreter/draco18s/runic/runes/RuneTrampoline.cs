using RunicInterpreter.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneTrampoline : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.SetSkip();
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('!', this);
			return this;
		}
	}
}