using RunicInterpreter.draco18s.runic.init;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneEquals : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			bool r = a.Equals(b);
			pointer.Push(r ? 1 : 0);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('=', this);
			return this;
		}
	}
}