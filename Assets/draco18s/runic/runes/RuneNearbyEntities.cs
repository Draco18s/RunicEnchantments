using Assets.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneNearbyEntities : IExecutableRune {
		public bool Execute(Pointer pointer, GameObject go) {
			object o = pointer.Pop();
			if(o is ValueType) {
				float rad = (float)Convert.ToDouble(o);

				if(pointer.GetMana() <= Mathf.CeilToInt(rad)) {
					pointer.Push(rad);
					return false;
				}
				pointer.DeductMana(Mathf.CeilToInt(rad));

				Collider[] colliders = Physics.OverlapSphere(go.transform.position, rad);
				foreach(Collider c in colliders) {
					pointer.Push(c.gameObject);
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('Q', this);
			return this;
		}
	}
}