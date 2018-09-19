using Assets.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneNegate : IExecutableRune {
		RuneMultiplication mult = new RuneMultiplication();
		public bool Execute(Pointer pointer, GameObject go) {
			object o = pointer.Pop();
			if(o is ValueType) {
				pointer.Push(o);
				pointer.Push(-1);
				mult.Execute(pointer, go);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('Z', this);
			return this;
		}
	}
}