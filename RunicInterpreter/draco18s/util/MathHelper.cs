
using RunicInterpreter.draco18s.math;
using System;
using System.Collections;

namespace RunicInterpreter.draco18s.util {
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
			if(Approximately((float)a, (float)b)) return NumericRelationship.EqualTo;
			if(a > b) return NumericRelationship.GreaterThan;
			return NumericRelationship.LessThan;
		}

		public static bool Approximately(float a, float b) {
			return (Math.Abs(a - b) <= 10*float.Epsilon);
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
			return Radians / ((float)Math.PI / 180f);
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
			return (int)Math.Floor(x / v) * v;
		}

		public static float snap(float pos, float v) {
			float x = pos;
			return (int)Math.Floor(x / v) * v;
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
				Angle += (float)Math.PI;
			while(Angle >= Math.PI)
				Angle -= (float)Math.PI;
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

		public static double AngleBetweenPointsDegreesDouble(float P1X, float P1Y, float P2X, float P2Y) {
			return AngleBetweenPointsRadiansDouble(P1X, P1Y, P2X, P2Y) * (180 / PID);
		}
		public static double AngleBetweenPointsRadiansDouble(float P1X, float P1Y, float P2X, float P2Y) {
			P2X -= P1X;
			P2Y -= P1Y;
			P1X = 0;
			P1Y = 0;
			return Math.Atan2((P2Y - P1Y), (P2X - P1X));
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

		public static void shuffleArray<T>(ref T[] array, System.Random random) {
			for(int i = array.Length - 1; i > 0; i--) {
				int index = random.Next(i + 1);
				T a = array[index];
				array[index] = array[i];
				array[i] = a;
			}
		}

		public static bool IsInteger(ValueType value) {
			return (value is SByte || value is Int16 || value is Int32
					|| value is Int64 || value is Byte || value is UInt16
					|| value is UInt32 || value is UInt64);
		}
	}
}