﻿using System;
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
			if(list.Count <= 1) return;
			T o = list[0];
			list.RemoveAt(0);
			list.Add(o);
		}

		public static void RotateListRight<T>(this List<T> list) {
			if(list.Count <= 1) return;
			list.Reverse();
			list.RotateListLeft();
			list.Reverse();
		}

		public static List<int> AllIndexesOf(this string str, string value) {
			if(String.IsNullOrEmpty(value))
				throw new ArgumentException("the string to find may not be empty", "value");
			List<int> indexes = new List<int>();
			for(int index = 0; ; index += value.Length) {
				index = str.IndexOf(value, index);
				if(index == -1)
					return indexes;
				indexes.Add(index);
			}
		}

		public static IEnumerable<string> ChunksUpto(this string str, int maxChunkSize) {
			for(int i = 0; i < str.Length; i += maxChunkSize)
				yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
		}

		public static string Reverse(this string s) {
			char[] charArray = s.ToCharArray();
			List<char> l = charArray.ToList();
			l.Reverse();
			return new string(l.ToArray());
		}

		public static string RotateLeft(this string s) {
			char[] charArray = s.ToCharArray();
			List<char> l = charArray.ToList();
			l.RotateListLeft();
			return new string(l.ToArray());
		}

		public static string RotateRight(this string s) {
			char[] charArray = s.ToCharArray();
			List<char> l = charArray.ToList();
			l.RotateListRight();
			return new string(l.ToArray());
		}
	}
}
