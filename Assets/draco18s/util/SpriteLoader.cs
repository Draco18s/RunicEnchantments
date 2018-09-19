using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.draco18s.config;

namespace Assets.draco18s.util {
	public static class SpriteLoader {
		private static Dictionary<String, Sprite> allSprites = new Dictionary<string, Sprite>();

		public static void setMaterial(GameObject go, String matName) {
			SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
			renderer.material = Resources.Load<Material>(matName);
		}

		public static GameObject gameObjectForResource(string texName) {
			Texture2D t = Resources.Load<Texture2D>(texName);
			return gameObjectForResource(texName, new Rect(0, 0, t.width, t.height));
		}

		public static GameObject gameObjectForResource(string texName, Rect size) {
			Sprite s = null;
			allSprites.TryGetValue(texName, out s);

			if (s == null)
			{
				Texture2D t = Resources.Load<Texture2D>(texName);
				s = Sprite.Create(t, size, Vector2.zero, Configuration.PIXELS_PER_UNIT, 0, SpriteMeshType.FullRect);
				allSprites.Add(texName, s);
			}
			GameObject go = new GameObject();
			SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
			renderer.sprite = s;
			//renderer.material = spriteMat;
			return go;
		}

		public static GameObject gameObjectForResource(Texture2D t, Rect size) {
			Sprite s = Sprite.Create(t, size, new Vector2(0.5f, 0.5f), Configuration.PIXELS_PER_UNIT, 0, SpriteMeshType.FullRect);
			GameObject go = new GameObject();
			SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
			renderer.sprite = s;
			//renderer.material = spriteMat;
			return go;
		}

		public static Sprite getSpriteForResource(string texName, Rect size) {
			Sprite s = null;
			allSprites.TryGetValue(texName, out s);
			Texture2D t;
			if (s == null)
			{
				t = Resources.Load<Texture2D>(texName);
				s = Sprite.Create(t, size, Vector2.zero, Configuration.PIXELS_PER_UNIT, 0, SpriteMeshType.FullRect);
				allSprites.Add(texName, s);
			}
			return s;
		}

		public static Texture2D getTextureForResource(string texName) {
			Texture2D t = null;
			t = Resources.Load<Texture2D>(texName);
			return t;
		}

		public static Sprite getSpriteForResource(string texName) {
			//Debug.Log(texName);
			Sprite s = null;
			allSprites.TryGetValue(texName, out s);

			if (s == null)
			{
				Texture2D t = Resources.Load<Texture2D>(texName);
				if(t == null) {
					Exception err = new Exception("Unable to load image file '" + texName + "'");
					Configuration.writeToErrorFile("MainThreadErrors.txt", err.ToString());
					return null;
				}

				Rect size = new Rect(0, 0, t.width, t.height);
				s = Sprite.Create(t, size, new Vector2(0.5f, 0.5f), Configuration.PIXELS_PER_UNIT, 0, SpriteMeshType.FullRect);
				allSprites.Add(texName, s);
			}
			return s;
		}

		public static Texture2D combineTexturesWithColor(Texture2D a, Texture2D b, Color color) {
			if(a.width != b.width || a.height != b.height) return null;
			Texture2D c = new Texture2D(a.width, a.height, TextureFormat.ARGB32, false);
			c.filterMode = FilterMode.Point;
			
			Color[] ap = a.GetPixels();
			Color[] bp = b.GetPixels();
			Color[] cp = new Color[ap.Length];
			float r1, g1, b1;
			float r2, g2, b2;
			float a1, a2, a3;
			for(int i = ap.Length - 1; i >= 0; i--) {
				//cp[i] = ap[i] + bp[i] * color;
				bp[i] = colorize(color, bp[i]);

				a1 = ap[i].a;
				r1 = ap[i].r;
				g1 = ap[i].g;
				b1 = ap[i].b;
				a2 = bp[i].a;
				r2 = bp[i].r;
				g2 = bp[i].g;
				b2 = bp[i].b;
				a3 = a2 + a1 * (1 - a2);
				if(a3 > 0) {
					r1 = (((r2 * a2) + (r1 * a1 * (1 - a2))) / a3);
					g1 = (((g2 * a2) + (g1 * a1 * (1 - a2))) / a3);
					b1 = (((b2 * a2) + (b1 * a1 * (1 - a2))) / a3);
				}
				else {
					r1 = g1 = b1 = 0;
				}
				cp[i] = new Color(r1, g1, b1, a3);
			}
			c.SetPixels(cp);
			c.Apply();
			return c;
		}

		private static Color colorize(Color cm, Color c2) {
			return colorize(cm, c2, false);
		}

		private static Color colorize(Color cm, Color c2, bool invert) {
			float[] pix = new float[3];
			float[] mul = new float[3];

			Color.RGBToHSV(c2, out pix[0], out pix[1], out pix[2]);
			Color.RGBToHSV(cm, out mul[0], out mul[1], out mul[2]);
			
			float v = (invert ? 1 - pix[2] : pix[2]);
			
			Color c3 = Color.HSVToRGB(mul[0], mul[1], v);
			c3.a = c2.a;
			return c3;// new Color(c3.r, c3.g, c3.b, c2.a);
		}
	}
}
