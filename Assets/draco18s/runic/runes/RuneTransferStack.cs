using Assets.draco18s.runic.init;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;
using Assets.draco18s.util;

namespace Assets.draco18s.runic.runes {
	public class RuneTransferStack : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			ReadOnlyCollection<Pointer> allpts = context.GetPointers();
			IEnumerable<Pointer> pts = allpts.Where(x => x != pointer && x.position.x == pointer.position.x && x.position.y == pointer.position.y);
			if(pts.Count() == 0) return false;
			if(pointer.GetStackSize() == 0) return false;
			object o = pointer.Pop();
			if(o is ValueType) {
				int v = (int)MathHelper.GetValue((ValueType)o);
				List<object> stack = new List<object>();
				for(;v>0;v--) {
					stack.Add(pointer.Pop());
				}
				stack.Reverse();
				foreach(Pointer p in pts) {
					PsuhStack(p, stack);
					p.SetSkip();
					//context.AdvancePointer(p);
				}
				return true;
			}
			return false;
		}

		private void PsuhStack(Pointer p, List<object> stack) {
			foreach(object o in stack) {
				p.Push(o);
			}
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('T', this);
			return this;
		}
	}
}