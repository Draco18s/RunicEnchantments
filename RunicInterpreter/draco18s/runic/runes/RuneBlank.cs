using RunicInterpreter.draco18s.runic.init;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneBlank : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(' ', this);
			return this;
		}
	}
}