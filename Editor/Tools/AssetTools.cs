#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Metimos
{
	public static class AssetTools
	{
		public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
		{
			string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");

			return guids.
				Select(AssetDatabase.GUIDToAssetPath).
				Select(AssetDatabase.LoadAssetAtPath<T>).
				Where(asset => asset != null).ToList();
		}
	}
}

#endif
