using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.util;
using System;
using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneFork : IExecutableRune {
		char c;
		Direction dir;
		public RuneFork(Direction d, char c) {
			this.c = c;
			dir = d;
		}
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				ValueType v = (ValueType)o;
				if(MathHelper.Compare(pointer.GetMana(), v) >= 0) {
					int d = (int)MathHelper.GetValue(v);
					pointer.DeductMana(d - 1);
					context.SpawnPointer(new Pointer(d,dir,pointer.position));
					return true;
				}
				else {
					pointer.Push(o);
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c, this);
			return this;
		}
	}
}