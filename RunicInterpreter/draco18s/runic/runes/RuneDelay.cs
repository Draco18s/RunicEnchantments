using RunicInterpreter.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneDelay : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {

			pointer.direction = context.GetModifiedDirection(context.GetModifier(pointer.position.x, pointer.position.y), pointer.direction);
			int j = context.GetDelayAmount(context.GetModifier(pointer.position.x, pointer.position.y));
			pointer.SetDelay(j + 1);
			pointer.SetSkip(j + 1);

			return false;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('y', this);
			return this;
		}
	}
}