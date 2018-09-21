using RunicInterpreter.draco18s.math;
using RunicInterpreter.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneDistance : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			Vector3 v1 = getVec3(a);
			Vector3 v2 = getVec3(b);
			pointer.Push((v1 - v2).magnitude);
			return true;
		}

		private Vector3 getVec3(object obj) {
			if(obj is Vector3) {
				return (Vector3)obj;
			}
			return Vector3.zero;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('j', this);
			return this;
		}
	}
}