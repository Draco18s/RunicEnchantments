using RunicInterpreter.draco18s.runic.runes;
using System;
using System.Collections;
using System.Collections.Generic;


namespace RunicInterpreter.draco18s.runic.init {
	public static class RuneRegistry {
		public static Dictionary<char,IExecutableRune> ALL_RUNES;

		public static void Initialize() {
			ALL_RUNES = new Dictionary<char, IExecutableRune>();
			for(int i = 0; i < 10; i++) {
				new RuneNumber(i,i.ToString()[0]).Register();
			}
			new RuneNumber(10, 'a').Register();
			new RuneNumber(11, 'b').Register();
			new RuneNumber(12, 'c').Register();
			new RuneNumber(13, 'd').Register();
			new RuneNumber(14, 'e').Register();
			new RuneNumber(15, 'f').Register();
			new RunePi(Math.PI, 'π').Register();
			new RunePi(Math.PI, 'P').Register();

			new RuneBlank().Register();
			new RuneEntrySimple(Direction.LEFT,  '<').Register();
			new RuneEntrySimple(Direction.RIGHT, '>').Register();
			new RuneEntrySimple(Direction.UP,    '^').Register();
			new RuneEntrySimple(Direction.DOWN,  'v').Register();
			new RuneOutput('$', false).Register();
			new RuneOutput('@', true).Register();
			new RuneAddition().Register();
			new RuneSubtraction().Register();
			new RuneMultiplication().Register();
			new RuneDivision().Register();
			new RuneModulo().Register();
			new RuneTerminator().Register();
			new RuneMinMana().Register();
			new RuneDiagonalReflector('/').Register();
			new RuneDiagonalReflector('\\').Register();
			new RuneDuplicate().Register();
			new RuneMana().Register();
			new RunePop().Register();
			new RuneSwap().Register();
			new RuneSwapN().Register();
			new RuneReverse().Register();
			new RuneLength().Register();
			new RuneEquals().Register();
			new RuneLessThan().Register();
			new RuneGreaterThan().Register();
			new RuneConditional().Register();
			new RuneTrampoline().Register();
			new RuneReflector(Direction.RIGHT, '|').Register();
			new RuneReflector(Direction.DOWN, '_').Register();
			new RuneReflectAll().Register();
			new RuneRotateStack(true, '{').Register();
			new RuneRotateStack(false, '}').Register();
			new RuneNegate().Register();
			new RuneDirection(Direction.LEFT,  '←').Register();
			new RuneDirection(Direction.RIGHT, '→').Register();
			new RuneDirection(Direction.UP,    '↑').Register();
			new RuneDirection(Direction.DOWN,  '↓').Register();
			new RuneDirection(Direction.LEFT,  'L').Register();
			new RuneDirection(Direction.RIGHT, 'R').Register();
			new RuneDirection(Direction.UP,    'U').Register();
			new RuneDirection(Direction.DOWN,  'D').Register();
			new RuneVec3().Register();
			new RuneDistance().Register();
			new RunePower().Register();
			new RuneTenHundred(10, 'X').Register();
			new RuneTenHundred(100, 'C').Register();
			new RuneTenHundred(1000, 'Y').Register();
			new RuneFork(Direction.LEFT,  '↤').Register();
			new RuneFork(Direction.RIGHT, '↦').Register();
			new RuneFork(Direction.UP,    '↥').Register();
			new RuneFork(Direction.DOWN,  '↧').Register();
			new RuneFork(Direction.LEFT,  'H').Register();
			new RuneFork(Direction.RIGHT, 'K').Register();
			new RuneFork(Direction.UP,    'I').Register();
			new RuneFork(Direction.DOWN,  'J').Register();
			new RuneFizzle().Register();
			new RuneDelay().Register();
			new RuneReadChar().Register();
			new RuneReadString().Register();
			new RuneToChar().Register();
			new RuneToValue().Register();
			new RuneReadCharContinuous().Register();

			new RuneSort().Register();
			new RuneConcatenate().Register();
			new RuneSplit().Register();
			new RuneReadInput().Register();
			new RuneTransferStack().Register();
			new RuneMathFunc().Register();
			new RunePopNewStack().Register();
			new RunePushNewStack().Register();
			new RuneEval().Register();
			new RuneBranchFunction().Register();
			new RuneReflection().Register();
			/*List<char> runeList = new List<char>();
			foreach(char c in ALL_RUNES.Keys) {
				runeList.Add(c);
			}
			runeList.Sort();
			for(int i = 0; i < runeList.Count; i++) {
				Console.Write(runeList[i]);
			}
			Console.WriteLine("");*/
		}

		public static char GetRuneChar(IExecutableRune rune) {
			foreach(KeyValuePair<char,IExecutableRune> pair in ALL_RUNES) {
				if(pair.Value == rune) return pair.Key;
			}
			if(rune is RuneCharLiteral) {
				return ((RuneCharLiteral)rune).c;
			}
			return ' ';
		}

		public static IExecutableRune GetRune(char cat) {
			IExecutableRune r;
			ALL_RUNES.TryGetValue(cat, out r);
			return r;
		}
	}
}