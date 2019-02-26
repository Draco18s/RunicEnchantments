using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RunePop : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
			if(modifier == '̹' || modifier == '͗') {
				pointer.PopDiscardStack();
			}
			else {
				pointer.Pop();
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('~', this);
			return this;
		}
	}
}