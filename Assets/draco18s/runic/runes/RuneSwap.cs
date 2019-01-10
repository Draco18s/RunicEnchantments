using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneSwap : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
			if(modifier == '̹') {
				pointer.SwapStacksStack();
			}
			else {
				if(pointer.GetStackSize() < 2) return true;
				object a = pointer.Pop();
				object b = pointer.Pop();
				pointer.Push(a);
				pointer.Push(b);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('S', this);
			return this;
		}
	}
}