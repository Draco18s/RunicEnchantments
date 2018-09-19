using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.runic.runes {
	public interface IExecutableRune {
		bool Execute(Pointer pointer, GameObject thisGo);
		IExecutableRune Register();
	}
}