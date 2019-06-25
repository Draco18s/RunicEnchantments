using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneConcatenate : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			if(context.GetModifier(pointer.position.x,pointer.position.y) == '͍') {
				string result = "";
				object o = pointer.Pop();
				while(o is char && pointer.GetStackSize() > 0) {
					result += (char)o;
					o = pointer.Pop();
				}
				if(o is char) result += (char)o;
				else pointer.Push(o);
				result = result.Reverse();
				pointer.Push(result);
			}
			else {
				object a = pointer.Pop();
				object b = pointer.Pop();
				string x = "";
				string y = "";
				if(a is ValueType || a is string)
					x = a.ToString();
				if(b is ValueType || b is string)
					y = b.ToString();
				pointer.Push(y + x);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('q', this);
			return this;
		}
	}
}