using UnityEngine;
using System;
using System.Collections;

namespace Assets.draco18s.util {
	public static class MathHelper {
		public enum NumericRelationship {
			GreaterThan = 1,
			EqualTo = 0,
			LessThan = -1
		};

		public static double PIOver180D = System.Math.PI / 180.0;
		public static double PID = 3.14159265358979323846264338327950288419716939937510;

		public static int getLayerForMask(int maskVal) {
			int j = 1;
			while((maskVal & 1) == 0) {
				maskVal /= 2;
				j++;
			}
			return j;
		}

		public static NumericRelationship Compare(ValueType v1, ValueType v2) {
			double a = GetValue(v1);
			double b = GetValue(v2);
			if(Mathf.Approximately((float)a, (float)b)) return NumericRelationship.EqualTo;
			if(a > b) return NumericRelationship.GreaterThan;
			return NumericRelationship.LessThan;
		}

		public static double GetValue(ValueType v1) {
			if(IsInteger(v1))
				return (int)v1;
			if(v1 is char)
				return (char)v1 - 0;
			else if(v1 is Vector3) {
				return ((Vector3)v1).magnitude;
			}
			else return Convert.ToDouble(v1);
		}

		public static float EaseLinear(float time, float from, float to, float duration) {
			time /= duration;
			float by = to - from;
			return by * time + from;
		}

		public static float EaseInQuadratic(float time, float from, float to, float duration) {
			time /= duration;
			float by = to - from;
			return by * time * time + from;
		}

		public static float EaseInCubic(float time, float from, float to, float duration) {
			time /= duration;
			float by = to - from;
			return by * time * time * time + from;
		}

		public static float EaseInQuartic(float time, float from, float to, float duration) {
			time /= duration;
			float by = to - from;
			return by * time * time * time * time + from;
		}

		public static float EaseOutQuadratic(float time, float from, float to, int duration) {
			time /= duration;
			float by = to - from;
			return -by * time * (time - 2) + from;
		}

		public static double DegreesToRadiansD(double Degrees) {
			return Degrees * PIOver180D;
		}

		public static float RadiansToDegrees(float Radians) {
			return Radians / ((float)Mathf.PI / 180f);
		}

		public static double SetAngleAndEnsureWithinRange(double newAngle, double angleCeiling) {
			double acutalAngle;

			acutalAngle = newAngle;
			if(acutalAngle < 0f)
				acutalAngle += angleCeiling;
			if(acutalAngle >= angleCeiling)
				acutalAngle -= angleCeiling;
			return acutalAngle;
		}

		public static Vector3 snap(Vector3 pos, int v) {
			float x = pos.x;
			float y = pos.y;
			float z = pos.z;
			x = snap(x, v);
			y = snap(y, v);
			z = snap(z, v);
			return new Vector3(x, y, z);
		}

		public static int snap(int pos, int v) {
			float x = pos;
			return Mathf.FloorToInt(x / v) * v;
		}

		public static float snap(float pos, float v) {
			float x = pos;
			return Mathf.FloorToInt(x / v) * v;
		}

		public static Vector3 clamp(Vector3 pos, Rect bounds) {
			pos.x = Math.Max(Math.Min(pos.x, bounds.xMax), bounds.xMin);
			pos.z = Math.Max(Math.Min(pos.z, bounds.yMax), bounds.yMin);
			return pos;
		}

		public static float SafeAngleAddDegrees(float Angle, float AmountToAdd) {
			Angle += AmountToAdd;
			while(Angle < 0f)
				Angle += 360f;
			while(Angle >= 360f)
				Angle -= 360f;
			return Angle;
		}

		public static double SafeAngleAddDegrees(double Angle, double AmountToAdd) {
			Angle += AmountToAdd;
			while(Angle < 0f)
				Angle += 360;
			while(Angle >= 360)
				Angle -= 360;
			return Angle;
		}

		public static float SafeAngleAddRadians(float Angle, float AmountToAdd) {
			Angle += AmountToAdd;
			while(Angle < 0f)
				Angle += Mathf.PI;
			while(Angle >= Mathf.PI)
				Angle -= Mathf.PI;
			return Angle;
		}

		public static double SafeAngleAddRadians(double Angle, double AmountToAdd) {
			Angle += AmountToAdd;
			while(Angle < 0)
				Angle += 2 * System.Math.PI;
			while(Angle >= 2 * System.Math.PI)
				Angle -= 2 * System.Math.PI;
			return Angle;
		}

		public static double AngleBetweenPointsDegreesDouble(Vector2 P1, Vector2 P2) {
			return AngleBetweenPointsRadiansDouble(P1.x, P1.y, P2.x, P2.y) * (180 / PID);
		}

		public static double AngleBetweenPointsDegreesDouble(float P1X, float P1Y, float P2X, float P2Y) {
			return AngleBetweenPointsRadiansDouble(P1X, P1Y, P2X, P2Y) * (180 / PID);
		}

		public static double AngleBetweenPointsRadiansDouble(Vector2 P1, Vector2 P2) {
			return Math.Atan2((P2.y - P1.y), (P2.x - P1.x));
		}

		public static double AngleBetweenPointsRadiansDouble(float P1X, float P1Y, float P2X, float P2Y) {
			P2X -= P1X;
			P2Y -= P1Y;
			P1X = 0;
			P1Y = 0;
			return Math.Atan2((P2Y - P1Y), (P2X - P1X));
		}

		public static int ApproxDistanceBetweenPointsFast(Vector2 P1, Vector2 P2, int ShortcutsBeyond) {
			return ApproxDistanceBetweenPointsFast(
				Mathf.RoundToInt(P1.x),
				Mathf.RoundToInt(P1.y),
				Mathf.RoundToInt(P2.x),
				Mathf.RoundToInt(P2.y),
				ShortcutsBeyond);
		}

		public static int ApproxDistanceBetweenPointsFast(int P1x, int P1y, int P2x, int P2y, Int32 ShortcutsBeyond) {
			Int32 dx = P1x - P2x;
			Int32 dy = P1y - P2y;

			if(dx < 0) dx = -dx;
			if(dy < 0) dy = -dy;

			if(ShortcutsBeyond > 0) {
				if(dx > ShortcutsBeyond || dy > ShortcutsBeyond) {
					if(dx > dy)
						return dx;
					else
						return dy;
				}
			}

			Int64 min, max, approx;

			if(dx < dy) {
				min = dx;
				max = dy;
			}
			else {
				min = dy;
				max = dx;
			}

			approx = (max * 1007) + (min * 441);
			if(max < (min << 4))
				approx -= (max * 40);

			// add 512 for proper rounding
			Int32 val = (Int32)((approx + 512) >> 10);
			if(val < 0)
				return -val;
			return val;
		}

		public static float DistanceBetweenPoints(Vector2 P1, Vector2 P2) {
			float xd = P2.x - P1.x;
			float yd = P2.y - P1.y;
			return (Mathf.Abs(Mathf.Sqrt(Mathf.Abs(
				(((long)xd * (long)xd) + ((long)yd * (long)yd))))));
		}

		public static float VeryBasicWrongRange(Vector2 P1, Vector2 P2) {
			float xDiff = P1.x - P2.x;
			if(xDiff < 0)
				xDiff = -xDiff;

			float yDiff = P1.y - P2.y;
			if(yDiff < 0)
				yDiff = -yDiff;

			if(xDiff > yDiff)
				return xDiff;
			else
				return yDiff;
		}

		public static double GetShortestAngleDeltaDegrees(double FromAngle, double ToAngle) {
			double deltaUp;
			double deltaDown;

			if(FromAngle <= ToAngle) {
				deltaUp = ToAngle - FromAngle;
				deltaDown = FromAngle + (360.0 - ToAngle);
			}
			else {
				deltaUp = (360.0 - FromAngle) + ToAngle;
				deltaDown = FromAngle - ToAngle;
			}

			if(Math.Abs(deltaUp) <= Math.Abs(deltaDown))
				return deltaUp;
			else
				return -deltaDown;
		}

		public static void GetDistanceAndAngle(Vector2 point, out double distance, out double angle) {
			distance = point.magnitude;
			angle = MathHelper.SafeAngleAddRadians(0, MathHelper.AngleBetweenPointsRadiansDouble(Vector2.zero, point));
		}

		public static Vector2 GetPointFromCircleCenterV(Vector2 Center, double DistanceFromCenter, double DegreesAngleFromCenter) {
			double baseX = DistanceFromCenter * Math.Cos(MathHelper.DegreesToRadiansD(DegreesAngleFromCenter));
			double baseY = DistanceFromCenter * Math.Sin(MathHelper.DegreesToRadiansD(DegreesAngleFromCenter));
			return new Vector2((float)(Center.x + baseX), (float)(Center.y + baseY));
		}

		public static void shuffleArray<T>(ref T[] array, System.Random random) {
			for(int i = array.Length - 1; i > 0; i--) {
				int index = random.Next(i + 1);
				T a = array[index];
				array[index] = array[i];
				array[i] = a;
			}
		}

		public static Vector2 VectorForAngle(float currentAngleRadians) {
			float sin = Mathf.Sin(currentAngleRadians);
			float cos = Mathf.Cos(currentAngleRadians);
			return new Vector2(sin, cos);
		}

		public static bool IsInteger(ValueType value) {
			return (value is SByte || value is Int16 || value is Int32
					|| value is Int64 || value is Byte || value is UInt16
					|| value is UInt32 || value is UInt64);
		}
	}
}