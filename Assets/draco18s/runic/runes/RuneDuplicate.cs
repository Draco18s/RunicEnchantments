﻿using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneDuplicate : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
			if(modifier == '̹' || modifier == '͗') {
				pointer.CloneTopSubStack();
			}
			else {
				object o = pointer.Pop();
				pointer.Push(o);
				pointer.Push(o);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(':', this);
			return this;
		}
	}
}