using RunicInterpreter.draco18s.runic.init;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneReverse : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.ReverseStack();
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('r', this);
			return this;
		}
	}
}