using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.util;
using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneRotateStack : IExecutableRune {
		bool rotLeft;
		char c;
		public RuneRotateStack(bool left, char c) {
			rotLeft = left;
			this.c = c;
		}

		public bool Execute(Pointer pointer, ExecutionContext context) {
			char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
			if(modifier == '̹' || modifier == '͗') {
				pointer.RotateStacksStack(rotLeft);
			}
			else if(modifier == '͍') {
				object o = pointer.Pop();
				if(o is string) {
					string s = (string)o;
					if(rotLeft)
						s = s.RotateLeft();
					else
						s = s.RotateRight();
					pointer.Push(s);
				}
				else {
					pointer.Push(o);
				}
			}
			else {
				pointer.RotateStack(rotLeft);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c, this);
			return this;
		}
	}
}