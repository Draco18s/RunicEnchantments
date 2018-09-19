using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.draco18s.util {
	public static class TypeExtensions {
		public static bool IsArrayOf<T>(this Type type) {
			return type == typeof(T[]);
		}

		public static void SetLayer(this GameObject obj, int layer, bool recursive) {
			obj.layer = layer;
			if(recursive) {
				foreach(Transform child in obj.transform) {
					child.gameObject.SetLayer(layer, recursive);
				}
			}
		}

		public static void DestroyAllChildren(this Transform trans) {
			for(int i = trans.childCount-1; i>=0; i--) {
				UnityEngine.Object.Destroy(trans.GetChild(i).gameObject);
			}
		}

		public static void RotateListLeft<T>(this List<T> list) {
			T o = list[0];
			list.RemoveAt(0);
			list.Add(o);
		}

		public static void RotateListRight<T>(this List<T> list) {
			list.Reverse();
			list.RotateListLeft();
			list.Reverse();
		}
	}
}
