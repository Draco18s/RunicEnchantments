using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.util;
using System;
using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneToValue : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				pointer.Push(MathHelper.GetValue((ValueType)o));
			}
			else if(o is string) {
				double d;
				if(double.TryParse((string)o, out d))
					pointer.Push(d);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('n', this);
			return this;
		}
	}
}