using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Metimos
{
	public static class BlendUtility
	{
		public static float Combine(BlendMode blendMode, float a, float b)
		{
			return blendMode switch
			{
				BlendMode.Add => Add(a, b),
				BlendMode.Burn => Burn(a, b),
				BlendMode.Difference => Difference(a, b),
				BlendMode.Multiply => Multiply(a, b),
				BlendMode.Normal => b,
				BlendMode.Overlay => Overlay(a, b),
				BlendMode.Screen => Screen(a, b),
				BlendMode.Subtract => Subtract(a, b),
				BlendMode.Divide => Divide(a, b),
				BlendMode.Average => Average(a, b),
				BlendMode.Exclusion => Exclusion(a, b),
				_ => a,
			};
		}
		
		public static double Combine(BlendMode blendMode, double a, double b)
		{
			return blendMode switch
			{
				BlendMode.Add => Add(a, b),
				BlendMode.Burn => Burn(a, b),
				BlendMode.Difference => Difference(a, b),
				BlendMode.Multiply => Multiply(a, b),
				BlendMode.Normal => b,
				BlendMode.Overlay => Overlay(a, b),
				BlendMode.Screen => Screen(a, b),
				BlendMode.Subtract => Subtract(a, b),
				BlendMode.Divide => Divide(a, b),
				BlendMode.Average => Average(a, b),
				BlendMode.Exclusion => Exclusion(a, b),
				_ => a,
			};
		}

		public static Color Combine(BlendMode blendMode, Color a, Color b)
		{
			return blendMode switch
			{
				BlendMode.Add => Add(a, b),
				BlendMode.Burn => Burn(a, b),
				BlendMode.Difference => Difference(a, b),
				BlendMode.Multiply => Multiply(a, b),
				BlendMode.Normal => b,
				BlendMode.Overlay => Overlay(a, b),
				BlendMode.Screen => Screen(a, b),
				BlendMode.Subtract => Subtract(a, b),
				BlendMode.Divide => Divide(a, b),
				BlendMode.Average => Average(a, b),
				BlendMode.Exclusion => Exclusion(a, b),
				_ => a,
			};
		}
		
		public static Color32 Combine(BlendMode blendMode, Color32 a, Color32 b)
		{
			return blendMode switch
			{
				BlendMode.Add => Add(a, b),
				BlendMode.Burn => Burn(a, b),
				BlendMode.Difference => Difference(a, b),
				BlendMode.Multiply => Multiply(a, b),
				BlendMode.Normal => b,
				BlendMode.Overlay => Overlay(a, b),
				BlendMode.Screen => Screen(a, b),
				BlendMode.Subtract => Subtract(a, b),
				BlendMode.Divide => Divide(a, b),
				BlendMode.Average => Average(a, b),
				BlendMode.Exclusion => Exclusion(a, b),
				_ => a,
			};
		}

		public static float Combine(BlendMode blendMode, float a, float b, float opacity) => Lerp(a, Combine(blendMode, a, b), opacity);
		public static double Combine(BlendMode blendMode, double a, double b, double opacity) => Lerp(a, Combine(blendMode, a, b), opacity);
		public static Color Combine(BlendMode blendMode, Color a, Color b, float opacity) => Lerp(a, Combine(blendMode, a, b), opacity);
		public static Color32 Combine(BlendMode blendMode, Color32 a, Color32 b, float opacity) => Lerp(a, Combine(blendMode, a, b), opacity);
		
		// Float implementations.
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Add(float a, float b) => a + b;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Subtract(float a, float b) => a - b;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Multiply(float a, float b) => a * b;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Divide(float a, float b) => a / b;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Difference(float a, float b) => Math.Abs(a - b);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Burn(float a, float b) => 1 - (1 - a) / b;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Overlay(float a, float b) => a > 0.5f ? 2f * a * b : 1f - 2f * (1f - a) * (1f - b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Screen(float a, float b) => 1f - (1f - a) * (1f - b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Average(float a, float b) => (a + b) * 0.5f;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Exclusion(float a, float b) => a + b - 2f * a * b;
		
		// Double implementations.
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Add(double a, double b) => a + b;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Subtract(double a, double b) => a - b;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Multiply(double a, double b) => a * b;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Divide(double a, double b) => a / b;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Difference(double a, double b) => Math.Abs(a - b);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Burn(double a, double b) => 1 - (1 - a) / b;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Overlay(double a, double b) => a > 0.5 ? 2 * a * b : 1 - 2 * (1 - a) * (1 - b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Screen(double a, double b) => 1 - (1 - a) * (1 - b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Average(double a, double b) => (a + b) * 0.5;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Exclusion(double a, double b) => a + b - 2 * a * b;
		
		// Color implementations.
		public static Color Add(Color a, Color b) => new(Add(a.r, b.r), Add(a.g, b.g), Add(a.b, b.b), Add(a.a, b.a));
		public static Color Subtract(Color a, Color b) => new(Subtract(a.r, b.r), Subtract(a.g, b.g), Subtract(a.b, b.b), Subtract(a.a, b.a));
		public static Color Multiply(Color a, Color b) => new(Multiply(a.r, b.r), Multiply(a.g, b.g), Multiply(a.b, b.b), Multiply(a.a, b.a));
		public static Color Divide(Color a, Color b) => new(Divide(a.r, b.r), Divide(a.g, b.g), Divide(a.b, b.b), Divide(a.a, b.a));
		public static Color Difference(Color a, Color b) => new(Difference(a.r, b.r), Difference(a.g, b.g), Difference(a.b, b.b), Difference(a.a, b.a));
		public static Color Burn(Color a, Color b) => new(Burn(a.r, b.r), Burn(a.g, b.g), Burn(a.b, b.b), Burn(a.a, b.a));
		public static Color Overlay(Color a, Color b) => new(Overlay(a.r, b.r), Overlay(a.g, b.g), Overlay(a.b, b.b), Overlay(a.a, b.a));
		public static Color Screen(Color a, Color b) => new(Screen(a.r, b.r), Screen(a.g, b.g), Screen(a.b, b.b), Screen(a.a, b.a));
		public static Color Average(Color a, Color b) => new(Average(a.r, b.r), Average(a.g, b.g), Average(a.b, b.b), Average(a.a, b.a));
		public static Color Exclusion(Color a, Color b) => new(Exclusion(a.r, b.r), Exclusion(a.g, b.g), Exclusion(a.b, b.b), Exclusion(a.a, b.a));
		
		// Color32 implementations.
		public static Color32 Add(Color32 a, Color32 b) => Add((Color)a, (Color)b);
		public static Color32 Subtract(Color32 a, Color32 b) => Subtract((Color)a, (Color)b);
		public static Color32 Multiply(Color32 a, Color32 b) => Multiply((Color)a, (Color)b);
		public static Color32 Divide(Color32 a, Color32 b) => Divide((Color)a, (Color)b);
		public static Color32 Difference(Color32 a, Color32 b) => Difference((Color)a, (Color)b);
		public static Color32 Burn(Color32 a, Color32 b) => Burn((Color)a, (Color)b);
		public static Color32 Overlay(Color32 a, Color32 b) => Overlay((Color)a, (Color)b);
		public static Color32 Screen(Color32 a, Color32 b) => Screen((Color)a, (Color)b);
		public static Color32 Average(Color32 a, Color32 b) => Average((Color)a, (Color)b);
		public static Color32 Exclusion(Color32 a, Color32 b) => Exclusion((Color)a, (Color)b);
		
		// Lerp.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float Lerp(float a, float b, float t) => a + (b - a) * t;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static double Lerp(double a, double b, double t) => a + (b - a) * t;
		
		public static Color Lerp(Color a, Color b, float t) => new(Lerp(a.r, b.r, t), Lerp(a.g, b.g, t), Lerp(a.b, b.b, t));
		public static Color32 Lerp(Color32 a, Color32 b, float t) => Lerp((Color)a, (Color)b, t);
	}
}