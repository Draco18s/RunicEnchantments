using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.runes {
	public interface IExecutableRune {
		bool Execute(Pointer pointer, ExecutionContext thisGo);
		IExecutableRune Register();
	}
}