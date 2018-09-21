using RunicInterpreter.draco18s.math;
using System.Collections;
using System.Collections.Generic;

namespace RunicInterpreter.draco18s.runic {
	public enum ParseErrorType {
		NONE,
		INVALID_CHARACTER,
		NO_ENTRY,
		NO_TERMINATOR
	}

	public struct ParseError {
		public ParseErrorType type;
		public Vector2Int pos;
		public char character;

		public ParseError(ParseErrorType type, Vector2Int pos, char character) {
			this.type = type;
			this.pos = pos;
			this.character = character;
		}
	}
}