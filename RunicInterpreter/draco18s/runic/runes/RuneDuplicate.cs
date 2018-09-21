using RunicInterpreter.draco18s.runic.init;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneDuplicate : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			pointer.Push(o);
			pointer.Push(o);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(':', this);
			return this;
		}
	}
}