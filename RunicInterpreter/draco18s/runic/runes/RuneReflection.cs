﻿using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.runic.runes;
using RunicInterpreter.draco18s.util;
using System;
using System.Globalization;

namespace RunicInterpreter.draco18s.runic.runes {
	public class RuneReflection : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			char modifier = context.GetModifier(pointer.position.x, pointer.position.y);
			int x = pointer.position.x;
			int y = pointer.position.y;
			if(modifier == '͍') {
				object a = pointer.Pop();
				object b = pointer.Pop();
				if(a is ValueType && b is ValueType) {
					x = (int)MathHelper.GetValue((ValueType)b);
					y = (int)MathHelper.GetValue((ValueType)a);
				}
				if(!context.IsValidPos(x, y)) {
					pointer.DeductMana(pointer.GetMana());
					return true;
				}
				char c1 = context.GetRune(x, y);
				char c2 = context.GetModifier(x, y);
				pointer.Push(c1);
				if(c2 != ' ')
					pointer.Push(c2);
				pointer.DeductMana(1);
			}
			else {
				object o = pointer.Pop();
				if(o is char) {
					char c = (char)o;
					if(pointer.GetStackSize() >= 2) {
						object a = pointer.Pop();
						object b = pointer.Pop();
						if(a is ValueType && b is ValueType) {
							x = (int)MathHelper.GetValue((ValueType)b);
							y = (int)MathHelper.GetValue((ValueType)a);
						}
						else {
							pointer.Push(b);
							pointer.Push(a);
						}
					}
					UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
					if(!context.IsValidPos(x, y)) {
						pointer.DeductMana(pointer.GetMana());
						return true;
					}
					if(uc == UnicodeCategory.NonSpacingMark || uc == UnicodeCategory.EnclosingMark || uc == UnicodeCategory.OtherNotAssigned) {
						if(c == '͏') {
							c = ' ';
						}
						context.SetModifier(x, y, c);
					}
					else {
						context.SetRune(x, y, c);
					}
					pointer.DeductMana(1);
				}
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('w', this);
			return this;
		}
	}
}