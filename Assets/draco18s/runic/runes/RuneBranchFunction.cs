using UnityEngine;
using System.Collections;
using Assets.draco18s.runic.init;
using System;
using Assets.draco18s.util;

namespace Assets.draco18s.runic.runes {
	public class RuneBranchFunction : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a != null && b != null) {
				if(a is ValueType && b is ValueType) {
					if(MathHelper.IsInteger((ValueType)a) && MathHelper.IsInteger((ValueType)b)) {
						int x = (int)MathHelper.GetValue((ValueType)b);
						int y = (int)MathHelper.GetValue((ValueType)a);
						char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
						context.AdvancePointer(pointer,false);
						if(modifier != '͍') {
							pointer.Push(pointer.position.x);
							pointer.Push(pointer.position.y);
						}
						pointer.position = new Vector2Int(x, y);
						pointer.direction = context.GetModifiedDirection(modifier, pointer.direction);
						return false;
					}
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('B', this);
			return this;
		}
	}
}