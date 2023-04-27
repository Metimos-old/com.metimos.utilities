using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metimos
{
	public static class GameObjectExtensions
	{
		public static void RemoveComponents(this GameObject gameObject)
		{
			Component[] components = gameObject.GetComponents<Component>();
			Dictionary<Type, List<Type>> dependencies = new();

			foreach (Component component in components)
			{
				if (component is Transform)
					continue;

				FindDependencies(component, dependencies);
			}

			RemoveDependencies(gameObject, dependencies);
		}

		private static void FindDependencies(Component component, IDictionary<Type, List<Type>> dependencies)
		{
			Type type = component.GetType();
			RequireComponent[] attributes = (RequireComponent[])type.GetCustomAttributes(typeof(RequireComponent), true);

			if (!dependencies.ContainsKey(type))
				dependencies.Add(type, new());

			if (attributes.Length == 0)
				return;

			foreach (RequireComponent attribute in attributes)
			{
				Type[] requiredTypes =
				{
					attribute.m_Type0,
					attribute.m_Type1,
					attribute.m_Type2,
				};

				foreach (Type requiredType in requiredTypes)
				{
					if (requiredType == null || requiredType == typeof(Transform))
						continue;

					if (!dependencies.TryGetValue(requiredType, out List<Type> types))
					{
						types = new();
						dependencies.Add(requiredType, types);
					}

					types.Add(type);
				}
			}
		}

		private static void RemoveDependencies(GameObject gameObject, Dictionary<Type, List<Type>> dependencies)
		{
			foreach ((Type type, List<Type> requiredTypes) in dependencies)
			{
				foreach (Type requiredType in requiredTypes)
				{
					Component component = gameObject.GetComponent(requiredType);

					if (component != null)
						UnityEngine.Object.Destroy(component);
				}

				UnityEngine.Object.Destroy(gameObject.GetComponent(type));
			}
		}
	}
}
