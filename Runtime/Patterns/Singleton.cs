using System;

namespace Metimos
{
	public class Singleton<T> where T : class, new()
	{
		protected Singleton()
		{
		}
		
		private static Lazy<T> s_instance;
		
		public static T Instance
		{
			get
			{
				if (s_instance?.Value == null)
				{
					s_instance = new(() => new());
				}

				return s_instance.Value;
			}
		}
		
		public static bool TryGetInstance(out T instance) => (instance = Instance) != null;
	}
}
