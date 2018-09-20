using Assets.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneTenHundred : IExecutableRune {
		int value;
		char c;
		private RuneMultiplication multi = new RuneMultiplication();
		public RuneTenHundred(int v, char c) {
			value = v;
			this.c = c;
		}
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				pointer.Push(o);
				pointer.Push(value);
				multi.Execute(pointer, context);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c, this);
			return this;
		}
	}
}