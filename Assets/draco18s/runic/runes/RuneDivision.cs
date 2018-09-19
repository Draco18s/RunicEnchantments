using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	internal class RuneDivision : IExecutableRune {

		public bool Execute(Pointer pointer, GameObject go) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a != null && b != null) {
				if(a is ValueType && b is ValueType) {
					if(MathHelper.IsInteger((ValueType)a) && MathHelper.IsInteger((ValueType)b)) {
						float c = (float)((int)b) / (int)a;
						pointer.Push(c);
					}
					if(a is Vector3 || b is Vector3) {
						if(a is Vector3 && b is Vector3) {
							pointer.Push(Vector3.Dot((Vector3)a, (Vector3)b));
						}
						else if(a is Vector3) {
							double d = Convert.ToDouble(b);
							if(Mathf.Approximately((float)d, 0)) {
								pointer.DeductMana(pointer.GetMana());
								return true;
							}
							pointer.Push(((Vector3)a) / (float)d);
						}
						else if(b is Vector3) {
							double d = Convert.ToDouble(a);
							if(Mathf.Approximately((float)d,0)) {
								pointer.DeductMana(pointer.GetMana());
								return true;
							}
							pointer.Push(((Vector3)b) / (float)d);
						}
					}
					else {
						double d = Convert.ToDouble(a);
						if(d == 0) {
							pointer.DeductMana(pointer.GetMana());
							return true;
						}
						double c = Convert.ToDouble(b) / d;
						pointer.Push(c);
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