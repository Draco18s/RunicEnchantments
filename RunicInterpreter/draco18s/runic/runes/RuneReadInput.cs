using RunicInterpreter.draco18s.runic.init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneReadInput : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			string s = "";
			do {
				char c = (char)Console.In.Read();
				s += c;
				if(char.IsWhiteSpace(c))
					break;
			} while(true);
			double d;
			if(double.TryParse(s, out d)) {
				pointer.Push(d);
			}
			else if(s.Length > 1) {
				pointer.Push(s);
			}
			else {
				pointer.Push((char)s[0]);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('i', this);
			return this;
		}
	}
}