using System.Runtime.CompilerServices;

namespace Metimos
{
	public static class Interpolator
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Linear(double a, double b, double t) => a + t * (b - a);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Linear(float a, float b, float t) => a + t * (b - a);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Hermite(double t) => t * t * (3 - 2 * t);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Hermite(float t) => t * t * (3 - 2 * t);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Quintic(double t) => t * t * t * (t * (t * 6 - 15) + 10);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Quintic(float t) => t * t * t * (t * (t * 6 - 15) + 10);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Cubic(double a, double b, double c, double d, double t)
		{
			double p = d - c - (b - a);
			double t2 = t * t;
			double t3 = t2 * t;
			
			return t3 * p + t2 * (b - a - p) + t * (c - b) + a;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Cubic(float a, float b, float c, float d, float t)
		{
			float p = d - c - (b - a);
			float t2 = t * t;
			float t3 = t2 * t;
			
			return t3 * p + t2 * (b - a - p) + t * (c - b) + a;
		}
	}
}