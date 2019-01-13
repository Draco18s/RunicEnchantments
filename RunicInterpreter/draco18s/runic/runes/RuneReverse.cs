using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.util;

namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneReverse : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
			if(modifier == '̹' || modifier == '͗') {
				pointer.ReverseStacksStack();
			}
			else if(modifier == '͍') {
				object o = pointer.Pop();
				if(o is string) {
					string s = (string)o;
					s = s.Reverse();
					pointer.Push(s);
				}
				else {
					pointer.Push(o);
				}
			}
			else {
				pointer.ReverseStack();
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('r', this);
			return this;
		}
	}
}