﻿using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneEquals : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object a = pointer.Pop();
			object b = pointer.Pop();
			bool r;
			if(a == null && b == null) r = true;
			else if(a == null || b == null) r = false;
			else if(a is ValueType && b is ValueType) r = MathHelper.NumericRelationship.EqualTo == MathHelper.Compare((ValueType)a, (ValueType)b);
			else r = a.Equals(b);

			char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
			if(modifier == '̸' || modifier == '͍') {
				r = !r;
			}
			pointer.Push(r ? 1 : 0);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('=', this);
			return this;
		}
	}
}