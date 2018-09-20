using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneOutput : IExecutableRune {
		bool dumpStack = false;
		char c;
		public RuneOutput(char c, bool fullStack) {
			this.c = c;
			dumpStack = fullStack;
		}
		public bool Execute(Pointer pointer, ExecutionContext context) {
			do {
				object o = pointer.Pop();
				if(o != null) {
					Debug.Log(o);
				}
			} while(dumpStack && pointer.GetStackSize() > 0);
			if(dumpStack) pointer.DeductMana(pointer.GetMana());
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c, this);
			return this;
		}
	}
}