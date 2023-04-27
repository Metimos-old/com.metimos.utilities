using System;

namespace Metimos
{
	public static class RandomExtensions
	{
		public static float Range(this Random random, float min, float max)
		{
			return (max - min) * (float)random.NextDouble() + min;
		}

		public static double Range(this Random random, double min, double max)
		{
			return (max - min) * random.NextDouble() + min;
		}
	}
}
