using RunicInterpreter.draco18s.runic.init;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RunePop : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.Pop();
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('~', this);
			return this;
		}
	}
}