using RunicInterpreter.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneNegate : IExecutableRune {
		RuneMultiplication mult = new RuneMultiplication();
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
				if(modifier == '͍') {
					if(o.Equals(0)) {
						pointer.Push(1);
					}
					else {
						pointer.Push(0);
					}
				}
				else {
					pointer.Push(o);
					pointer.Push(-1);
					mult.Execute(pointer, context);
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('Z', this);
			return this;
		}
	}
}