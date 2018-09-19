using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace Assets.draco18s.config {
	public static class Configuration {
		public static float PIXELS_PER_UNIT = 100;
		public static IFormatProvider NumberFormat;

		public static void writeToErrorFile(string v1, string v2) {
			//Debug.Log(v2);
		}
	}
}