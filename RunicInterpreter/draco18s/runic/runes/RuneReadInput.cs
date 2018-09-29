using RunicInterpreter.draco18s.runic.init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneReadInput : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			StringBuilder sb = new StringBuilder();
			bool forceread = false;
			while(Console.In.Peek() != -1) {
				char c = (char)Console.In.Read();
				if(c == '\r') continue;
				if(!forceread && char.IsWhiteSpace(c)) break;
				if(!forceread && c == '\\') {
					forceread = true;
				}
				else {
					forceread = false;
					sb.Append(c);
				}
			}

			double d;
			string s = sb.ToString();
			if(double.TryParse(s, out d)) {
				pointer.Push(d);
			}
			else if(sb.Length > 1) {
				pointer.Push(s);
			}
			else if(sb.Length > 0) {
				pointer.Push(s[0]);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('i', this);
			return this;
		}
	}
}