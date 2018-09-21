using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneIsEnemy : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			pointer.Push(o);
			if(o is GameObject) {
				if(context.self.layer != ((GameObject)o).layer) {
					pointer.Push(1);
				}
				else {
					pointer.Push(0);
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('E', this);
			return this;
		}
	}
}