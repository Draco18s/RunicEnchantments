using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneReverse : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
			if(modifier == '̹') {
				pointer.ReverseStacksStack();
			}
			else {
				pointer.ReverseStack();
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('r', this);
			return this;
		}
	}
}