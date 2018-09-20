﻿using Assets.draco18s.runic.init;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneDiagonalReflector : IExecutableRune {
		protected char c;
		public RuneDiagonalReflector(char c) {
			this.c = c;
		}
		public bool Execute(Pointer pointer, ExecutionContext context) {
			int dir = (int)pointer.direction;
			if(this.c == '/') {
				if(dir % 2 == 0) {
					int a = 2 - dir;
					pointer.direction = DirectionHelper.Reflect((Direction)a);
				}
				else {
					int a = 4 - dir;
					pointer.direction = DirectionHelper.Reflect((Direction)a);
				}
			}
			else {
				if(dir <= 1) {
					pointer.direction = DirectionHelper.RotateCCW(pointer.direction);
				}
				else {
					pointer.direction = DirectionHelper.RotateCW(pointer.direction);
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(c, this);
			return this;
		}
	}
}