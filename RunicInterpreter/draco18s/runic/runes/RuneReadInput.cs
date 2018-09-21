using RunicInterpreter.draco18s.runic.init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneReadInput : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.Push(Console.In.Read());
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('i', this);
			return this;
		}
	}
}