using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.util;
using System;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneLessThan : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a is ValueType && b is ValueType) {
				MathHelper.NumericRelationship q = MathHelper.Compare((ValueType)b, (ValueType)a);
				bool r = q == MathHelper.NumericRelationship.LessThan;
				char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
				if(modifier == '̸' || modifier == '͍') {
					r = !r;
				}
				pointer.Push(r ? 1 : 0);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('(', this);
			return this;
		}
	}
}