﻿using Assets.draco18s.runic.init;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.draco18s.runic.runes {
	public class RuneReadInput : IExecutableRune {
		public bool Execute(Pointer pointer, ExecutionContext context) {
			context.ReadInput(pointer);
			return true;
		}

		public IExecutableRune Register() {
			RuneRegistry.ALL_RUNES.Add('i', this);
			return this;
		}
	}
}