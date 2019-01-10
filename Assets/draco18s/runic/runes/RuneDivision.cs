using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	internal class RuneDivision : IExecutableRune {

		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a != null && b != null) {
				if(a is ValueType && b is ValueType) {
					if(MathHelper.IsInteger((ValueType)a) && MathHelper.IsInteger((ValueType)b)) {
						float c = (float)MathHelper.GetValue((ValueType)b) / (int)MathHelper.GetValue((ValueType)a);
						pointer.Push(c);
					}
					else if(a is Vector3 || b is Vector3) {
						if(a is Vector3 && b is Vector3) {
							pointer.Push(Vector3.Dot((Vector3)a, (Vector3)b));
						}
						else if(a is Vector3) {
							double d = MathHelper.GetValue((ValueType)b);
							if(Mathf.Approximately((float)d, 0)) {
								pointer.DeductMana(pointer.GetMana());
								return true;
							}
							pointer.Push(((Vector3)a) / (float)d);
						}
						else if(b is Vector3) {
							double d = MathHelper.GetValue((ValueType)a);
							if(Mathf.Approximately((float)d,0)) {
								pointer.DeductMana(pointer.GetMana());
								return true;
							}
							pointer.Push(((Vector3)b) / (float)d);
						}
					}
					else {
						double d = MathHelper.GetValue((ValueType)a);
						if(d == 0) {
							pointer.DeductMana(pointer.GetMana());
							return true;
						}
						double c = MathHelper.GetValue((ValueType)b) / d;
						pointer.Push(c);
					}
				}
				else if(a is ValueType && b is string) {
					string s = (string)b;
					int n = (int)MathHelper.GetValue((ValueType)a);
					foreach(string chk in s.ChunksUpto(n)) {
						pointer.Push(chk);
					}
				}
				else {

				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add(',', this);
			return this;
		}
	}
}