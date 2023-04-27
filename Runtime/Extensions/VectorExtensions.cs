using System;
using UnityEngine;

namespace Metimos
{
	public static class VectorExtensions
	{
		private static readonly System.Random s_random = new();
		
		public static float Average(this Vector2 vector) => (vector.x + vector.y) / 2f;
		public static bool CompareEquals(this Vector3 vector, float magnitude) => Mathf.Approximately(vector.magnitude, magnitude);
		public static bool CompareSqrEquals(this Vector3 vector, float magnitude) => Mathf.Approximately(vector.sqrMagnitude, magnitude);
		public static bool CompareEquals(this Vector3 vector, Vector3 target) => Mathf.Approximately(vector.x, target.x) && Mathf.Approximately(vector.y, target.y) && Mathf.Approximately(vector.z, target.z);
		public static bool CompareInt(this Vector3 source, Vector3 target) => (int)source.x == (int)target.x && (int)source.y == (int)target.y && (int)source.z == (int)target.z;
		public static bool CompareInt(this Vector2 vector, Vector2 target) => (int)vector.x == (int)target.x && (int)vector.y == (int)target.y;
		public static bool InRange(this Vector2 vector, float value) => Math.Min(vector.x, vector.y) <= value && Math.Max(vector.x, vector.y) >= value;
		public static Vector2 TruncateY(this Vector3 vector) => new(vector.x, vector.z);
		public static Vector2 FlipXY(this Vector2 vector) => new Vector3(vector.y, vector.x);
		public static Vector3 FlipXZ(this Vector3 vector) => new(vector.z, vector.y, vector.x);
		public static Vector3 InsertY(this Vector2 vector, float y = 0) => new(vector.x, y, vector.y);

		public static Vector3 Merge(this Vector3 vector, Vector3 target, params bool[] useChannels)
		{
			for (int channelIndex = 0; channelIndex < useChannels.Length; channelIndex++)
				if (useChannels[channelIndex])
					vector[channelIndex] = target[channelIndex];

			return vector;
		}

		public static float Random(this Vector2 vector, System.Random random = null)
		{
			random ??= s_random;
			
			float min = Mathf.Min(vector.x, vector.y);
			float max = Mathf.Max(vector.x, vector.y);

			return (max - min) * (float)random.NextDouble() + min;
		}

		public static int Random(this Vector2Int vector, System.Random random = null)
		{
			random ??= s_random;
			
			int min = Math.Min(vector.x, vector.y);
			int max = Math.Max(vector.x, vector.y);

			return random.Next(min, max + 1);
		}

		public static float Range(this Vector2 vector, float time)
		{
			return Mathf.Lerp(vector.x, vector.y, time);
		}

		public static Vector3 RotateAround(this Vector3 point, Vector3 pivot, Quaternion angle)
		{
			Vector3 finalPos = point - pivot;
			finalPos = angle * finalPos;
			finalPos += pivot;

			return finalPos;
		}

		public static void ToShortArc(ref this Vector3 vector)
		{
			vector.Set(
				vector.x.ToShortArc(),
				vector.y.ToShortArc(),
				vector.z.ToShortArc()
			);
		}
	}
}
