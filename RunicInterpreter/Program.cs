using Coroutines;
using RunicInterpreter.draco18s.runic;
using RunicInterpreter.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RunicInterpreter {
	class Program {
		static void Main(string[] args) {
			CoroutineRunner runner = new CoroutineRunner();
			RuneRegistry.Initialize();
			bool keeprunning = true;

			string codeIn = File.ReadAllText(args[1]);
			//string codeIn = "50,$;";
			CoroutineHandle handle = runner.Run(Execute(codeIn));
			while(keeprunning) {
				float deltaTime = 0.1f;
				runner.Update(deltaTime);
				keeprunning = handle.IsRunning;
			}
		}

		private static IEnumerator Execute(string code) {
			yield return null;
			draco18s.runic.ExecutionContext context;
			ParseError err = Parser.Parse(code, out context);
			if(err.type != ParseErrorType.NONE || context == null) {
				ShowError(err);
				yield break;
			}
			context.Initialize();
			bool continueExecuting = false;
			int counter = 0;
			do {
				counter++;
				continueExecuting = context.Tick();
				yield return null;
			} while(continueExecuting && counter < 10000);
		}

		private static void ShowError(ParseError err) {
			Console.Error.WriteLine("Parser error: " + err.type.ToString() + " at position " + err.pos.ToString() + ", character '" + err.character + "'");
		}
	}
}
