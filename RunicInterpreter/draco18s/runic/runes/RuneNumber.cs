using RunicInterpreter.draco18s.runic.init;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneNumber : IExecutableRune {
		public int value;
		char c;

		public RuneNumber(int v, char c) {
			value = v;
			this.c = c;
		}

		public bool Execute(Pointer pointer, ExecutionContext context) {
			pointer.Push(value);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c,this);
			return this;
		}
	}
}