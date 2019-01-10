using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	internal class RuneSubtraction : IExecutableRune {

		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			if(a != null && b != null) {
				if(a is ValueType && b is ValueType) {
					if(MathHelper.IsInteger((ValueType)a) && MathHelper.IsInteger((ValueType)b)) {
						int c = (int)b - (int)a;
						pointer.Push(c);
					}
					else if(a is Vector3 && b is Vector3) {
						pointer.Push(((Vector3)b - (Vector3)a));
					}
					else {
						double c = MathHelper.GetValue((ValueType)b) - MathHelper.GetValue((ValueType)a);
						pointer.Push(c);
					}
				}
				else if(a is ValueType && b is string) {
					int n = (int)MathHelper.GetValue((ValueType)a);
					string s = (string)b;
					if(n > 0) {
						string first = s.Substring(0, n);
						string second = s.Substring(n, s.Length - n);
						if(first.Length > 0)
							pointer.Push(first);
						if(second.Length > 0)
							pointer.Push(second);
					}
					else if(n < 0) {
						n *= -1;
						string second = s.Substring(0, s.Length - n);
						pointer.Push(second);
					}
					else {
						pointer.Push(s);
					}
				}
				else {

				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('-', this);
			return this;
		}
	}
}