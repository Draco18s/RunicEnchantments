using RunicInterpreter.draco18s.math;
using RunicInterpreter.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RunePower : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a is ValueType && b is ValueType) {
				if(a is Vector3 || b is Vector3) {
				}
				else {
					double x = Convert.ToDouble(a);
					double y = Convert.ToDouble(b);
					pointer.Push(Math.Pow(y, x));
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('p', this);
			return this;
		}
	}
}