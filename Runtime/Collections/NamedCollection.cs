using System.Collections.Generic;
using UnityEngine;

namespace Metimos
{
	public abstract class NamedCollection
	{
		private static readonly Dictionary<string, NamedCollection> _names = new();
		
		public static string FormatKey(params string[] names)
		{
			return string.Join('.', names);
		}

		public static NamedCollection Get(string key)
		{
			return _names.TryGetValue(key, out NamedCollection collection) ? collection : null;
		}

		public virtual void Cache(string rootName)
		{
			string key = FormatKey(rootName, GetType().Name, GetName());

			if (!_names.TryAdd(key, this))
				Debug.LogWarningFormat("{0} is not unique: {1}", GetType().BaseType?.Name ?? "?", key);
		}

		public abstract string GetName();
	}

	internal static class Extension
	{
		public static void Cache(this IEnumerable<NamedCollection> collections, string name)
		{
			foreach (NamedCollection collection in collections)
				collection.Cache(name);
		}
	}
}
