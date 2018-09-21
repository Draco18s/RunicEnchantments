using RunicInterpreter.draco18s.math;
using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.util;
using System;


namespace RunicInterpreter.draco18s.runic.runes {
	internal class RuneAddition : IExecutableRune {

		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a != null && b != null) {
				if(a is ValueType && b is ValueType) {
					if(MathHelper.IsInteger((ValueType)a) && MathHelper.IsInteger((ValueType)b)) {
						int c = (int)a + (int)b;
						pointer.Push(c);
					}
					else if(a is Vector3 && b is Vector3) {
						pointer.Push(((Vector3)a + (Vector3)b));
					}
					else {
						double c = MathHelper.GetValue((ValueType)a) + MathHelper.GetValue((ValueType)b);
						pointer.Push(c);
					}
				}
				else {

				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('+',this);
			return this;
		}
	}
}