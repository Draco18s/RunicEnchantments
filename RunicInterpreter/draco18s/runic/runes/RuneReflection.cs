using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.runic.runes;
using System.Globalization;

namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneReflection : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is char) {
				char c = (char)o;
				int x = pointer.position.x;
				int y = pointer.position.y;
				if(pointer.GetStackSize() >= 2) {
					object a = pointer.Pop();
					object b = pointer.Pop();
					if(a is ValueType && b is ValueType) {
						x = (int)MathHelper.GetValue((ValueType)b);
						y = (int)MathHelper.GetValue((ValueType)a);
					}
					else {
						pointer.Push(b);
						pointer.Push(a);
					}
				}
				UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
				if(uc == UnicodeCategory.NonSpacingMark || uc == UnicodeCategory.EnclosingMark || uc == UnicodeCategory.OtherNotAssigned) {
					if(c == '͏') {
						c = ' ';
					}
					context.SetModifier(x, y, c);
				}
				else {
					context.SetRune(x, y, c);
				}
				pointer.DeductMana(1);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('w', this);
			return this;
		}
	}
}