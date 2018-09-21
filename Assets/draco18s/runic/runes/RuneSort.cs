using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using System.Collections.Generic;

namespace Assets.draco18s.runic.runes {
	public class RuneSort : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			int cost = pointer.GetStackSize();
			if(pointer.GetMana() <= cost) return false;
			pointer.DeductMana(cost);
			List<ValueType> list = new List<ValueType>();
			while(pointer.GetStackSize() > 0) {
				object o = pointer.Pop();
				if(o is ValueType)
					list.Add((ValueType)o);
				else {
					pointer.Push(o);
					break;
				}
			}
			list.Sort((x, y) => (int)MathHelper.Compare(y, x));
			for(int i = 0; i < list.Count; i++)
				pointer.Push(list[i]);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('o', this);
			return this;
		}
	}
}