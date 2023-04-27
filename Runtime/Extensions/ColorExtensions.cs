using UnityEngine;

namespace Metimos
{
	public static class ColorExtensions
	{
		private static readonly Color[] s_colors =
		{
			new(1f, 0f, 0f, 1f),
			new(0f, 1f, 0f, 1f),
			new(0f, 0f, 1f, 1f),
			new(1f, 1f, 0f, 1f),
			new(0f, 1f, 1f, 1f),
			new(1f, 0f, 1f, 1f),
			new(1f, 0.5f, 0f, 1f),
			new(1f, 0f, 0.5f, 1f),
			new(0.5f, 0f, 1f, 1f),
			new(0.5f, 1f, 0f, 1f),
		};
		
		public static Color WithAlpha(this Color color, float alpha)
		{
			color.a = alpha;
			return color;
		}

		public static Color IndexToColor(int index)
		{
			return s_colors[Mathf.Abs(index) % s_colors.Length];
		}
	}
}