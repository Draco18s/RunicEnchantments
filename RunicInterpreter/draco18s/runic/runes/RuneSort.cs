﻿using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneSort : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
			if(modifier == '͍') {
				object o = pointer.Pop();
				if(o is string) {
					string s = (string)o;
					int cost = Math.Max(s.Length - 10, 1);
					pointer.DeductMana(cost);
					List<char> list = (s).ToCharArray().ToList();
					list.Sort((x, y) => (int)MathHelper.Compare(x, y));
					s = new string(list.ToArray());
					pointer.Push(s);
				}
				else {
					pointer.Push(o);
				}
			}
			else {
			int cost = Math.Max(pointer.GetStackSize()-10,1);
			if(pointer.GetMana() <= cost) return false;
			pointer.DeductMana(cost);
			List<ValueType> list = new List<ValueType>();
			while(pointer.GetStackSize() > 0) {
				object o = pointer.Pop();
				/*if(IsGenericList(o)) {
					List<object> myAnythingList = (o as IEnumerable<object>).Cast<object>().ToList();
					myAnythingList.Sort();
					break;
				}*/
				if(o is ValueType)
					list.Add((ValueType)o);
				else {
					pointer.Push(o);
					break;
				}
			}
			list.Sort((x, y) => (int)MathHelper.Compare(y, x));
			for(int i = 0; i < list.Count; i++)
				pointer.Push(list[i]);
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('o', this);
			return this;
		}

		public bool IsGenericList(object o) {
			var oType = o.GetType();
			return (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
		}
	}
}