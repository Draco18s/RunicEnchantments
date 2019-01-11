using Assets.draco18s.runic.init;
using Assets.draco18s.util;
using System;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public class RuneEval : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			if(o is ValueType) {
				int j = (int)MathHelper.GetValue((ValueType)o);
				string s = WordDictionary.GetWord(j);
				if(s != null)
					pointer.Push(s);
			}
			else if(o is string) {
				string str = (string)o;
				int size;
				//pointer.DeductMana((int)Math.Ceiling(Math.Log(str.Length)));
				Func<bool> execution = context.Eval(pointer, str, out size);
				if(size > 5) {
					double n = Math.Log(size-5);
					pointer.DeductMana((int)Math.Floor(n * n));
				}
				pointer.Push(execution);
				return false;
				//Coroutine execution = context.StartCoroutine(Execute(transform.Find("CodeField").GetComponent<InputField>().text));
			}
			else if(o is Func<bool>) {
				Func<bool> isDone = (Func<bool>)o;
				bool ret = isDone();
				if(!ret)
					pointer.Push(o);
				return ret;
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('E', this);
			return this;
		}
	}
}