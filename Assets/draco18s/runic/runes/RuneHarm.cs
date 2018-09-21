using Assets.draco18s.generpg;
using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneHarm : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object o = pointer.Pop();
			if(o is GameObject && a is ValueType) {
				int amt = (int)MathHelper.GetValue((ValueType)a);
				GameObject go = (GameObject)o;
				IEntity ent = go.GetComponent<IEntity>();
				ITemporary tmp = go.GetComponent<ITemporary>();
				if(ent != null) {
					ent.Harm(amt);
				}
				else if(tmp != null) {
					tmp.DoDestroy(amt);
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('h', this);
			return this;
		}
	}
}