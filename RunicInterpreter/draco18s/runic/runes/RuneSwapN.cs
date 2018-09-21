using RunicInterpreter.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;

using RunicInterpreter.draco18s.util;

namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneSwapN : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				int n = (int)Convert.ToDouble(o);
				List<object> list = new List<object>();
				for(int i = 0; i < n; i++) {
					list.Add(pointer.Pop());
				}
				list.Reverse();
				list.RotateListRight();
				for(int i = 0; i < n; i++) {
					pointer.Push(list[i]);
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('s', this);
			return this;
		}
	}
}