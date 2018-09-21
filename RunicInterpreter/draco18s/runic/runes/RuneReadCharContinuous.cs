using RunicInterpreter.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneReadCharContinuous : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.SetReadType(Pointer.ReadType.READ_CHAR_CONTINUOUS);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('`', this);
			return this;
		}
	}
}