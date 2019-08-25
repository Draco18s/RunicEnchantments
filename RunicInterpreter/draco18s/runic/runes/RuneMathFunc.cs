using RunicInterpreter.draco18s.runic.init;
using RunicInterpreter.draco18s.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunicInterpreter.draco18s.runic.runes {
	class RuneMathFunc : IExecutableRune {
		Random rand = new Random();
		public bool Execute(Pointer pointer, ExecutionContext context) {
			object o = pointer.Pop();
			object v = pointer.Pop();
			if(o is ValueType && v is ValueType) {
				char c = (char)MathHelper.GetValue((ValueType)o);
				double x = MathHelper.GetValue((ValueType)v);
				switch(c) {
					case 'C':
						pointer.Push(Math.Cos(x));
						break;
					case 'S':
						pointer.Push(Math.Sin(x));
						break;
					case 'T':
						pointer.Push(Math.Tan(x));
						break;
					case 'e':
						pointer.Push(Math.Exp(x));
						break;
					case 'l':
						pointer.Push(Math.Log(x));
						break;
					case 'L':
						pointer.Push(Math.Log10(x));
						break;
					case 'f':
						pointer.Push(Math.Floor(x));
						break;
					case 'c':
						pointer.Push(Math.Ceiling(x));
						break;
					case 'r':
						pointer.Push(Math.Round(x));
						break;
					case '|':
						pointer.Push(Math.Abs(x));
						break;
					case 'q':
						pointer.Push(Math.Sqrt(x));
						break;
					case 'i':
						pointer.Push(Math.Asin(x));
						break;
					case 'o':
						pointer.Push(Math.Acos(x));
						break;
					case 'a':
						pointer.Push(Math.Atan(x));
						break;
					case 'R':
						pointer.Push(rand.Next((int)x));
						break;
					case '+':
						char[] digits = x.ToString().ToCharArray();
						int sum = 0;
						foreach(char d in digits) {
							sum += (d-48);
						}
						pointer.Push(sum);
						break;
					case '*':
						digits = x.ToString().ToCharArray();
						sum = 1;
						foreach(char d in digits) {
							sum *= (d - 48);
						}
						pointer.Push(sum);
						break;
					case '!':
						int number = (int)x;
						if(number == 0) {
							pointer.Push(1);
							break;
						}
						double fact = number;
						for(int i = number - 1; i >= 1; i--) {
							fact *= i;
						}
						pointer.Push(fact);
						break;
					case '‼':
						number = (int)x;
						if(number == 0) {
							pointer.Push(1);
							break;
						}
						fact = number;
						for(int i = number - 2; i >= 1; i-=2) {
							fact *= i;
						}
						pointer.Push(fact);
						break;
					case 'P':
						pointer.Push(IsPrime((int)x) ? 1 : 0);
						break;
					case '-':
						pointer.Push(Math.Sign(x));
						break;
					case '.':
						pointer.Push(Math.Truncate(x));
						break;
				}
			}
			return true;
		}

		private bool IsPrime(int number) {
			if(number < 2)
				return false;
			if(number == 2)
				return true;
			if(number % 2 == 0)
				return false;
			if(number % 3 == 0)
				return false;
			double sq = Math.Sqrt(number);
			for(int i = 5; i <= sq; i += 6) {
				if(number % i == 0)
					return false;
				if(number % (i+2) == 0)
					return false;
			}
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('A', this);
			return this;
		}
	}
}