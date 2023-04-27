using System;

namespace Metimos
{
	public interface IRandom
	{
		public float Probability { get; }

		private static readonly Random s_random = new();

		public static void GetWeightedAverage<T>(T[] array, out float sum, out float[] weights) where T : IRandom
		{
			sum = 0f;
			weights = new float[array.Length];

			for (int i = 0; i < array.Length; i++)
			{
				T item = array[i];
				float weight = item.Probability;
				
				sum += weight;
				weights[i] = weight;
			}
		}

		public static T Get<T>(T[] array) where T : IRandom
		{
			GetWeightedAverage(array, out float sum, out float[] weights);
			
			float random = (float)s_random.NextDouble() * sum;

			for (int i = 0; i < weights.Length; i++)
			{
				float weight = weights[i];
				if (random > weight) continue;
				
				return array[i];
			}

			return array[^1];
		}
		
		public static T[] Get<T>(T[] array, int count) where T : IRandom
		{
			GetWeightedAverage(array, out float sum, out float[] weights);
			
			T[] items = new T[count];

			for (int i = 0; i < count; i++)
			{
				float random = (float)s_random.NextDouble() * sum;

				for (int j = 0; j < weights.Length; j++)
				{
					float weight = weights[j];
					if (random > weight) continue;
					
					items[i] = array[j];
					sum -= weight;
					
					break;
				}
			}

			return items;
		}
	}
}