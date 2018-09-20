using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneReflector : IExecutableRune {
		Direction dir;
		char c;
		public RuneReflector(Direction dir, char c) {
			this.dir = dir;
			this.c = c;
		}
		public bool Execute(Pointer pointer, ExecutionContext context) {
			if(pointer.direction == dir || pointer.direction == DirectionHelper.Reflect(dir)) pointer.direction = DirectionHelper.Reflect(pointer.direction);

			
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c, this);
			return this;
		}
	}
}