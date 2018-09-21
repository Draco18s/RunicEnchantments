using RunicInterpreter.draco18s.runic.init;


namespace RunicInterpreter.draco18s.runic.runes {
	internal class RuneTerminator : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.DeductMana(pointer.GetMana());
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(';',this);
			return this;
		}
	}
}