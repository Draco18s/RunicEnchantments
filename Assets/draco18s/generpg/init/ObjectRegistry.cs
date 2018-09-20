using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.generpg.init {
	public class ObjectRegistry {
		public static Dictionary<string, GameObject> ALL_PREFABS;

		public static void Initialize() {
			ALL_PREFABS = new Dictionary<string, GameObject>();
			ALL_PREFABS.Add("cube", GameObject.CreatePrimitive(PrimitiveType.Cube));
			ALL_PREFABS.Add("arrow", Resources.Load<GameObject>("prefabs/arrow"));
		}

		public static GameObject GetObject(string o) {
			return ALL_PREFABS[o];
		}
	}
}