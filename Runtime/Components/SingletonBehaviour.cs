using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Metimos
{
	public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static bool IsInstanced => s_instances.Contains(typeof(T));

		[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeNullComparison")]
		public static T Instance
		{
			get
			{
				if (IsInstanced) return s_instance;
				
				s_instance = FindObjectOfType<T>();
				s_instances.Add(typeof(T));

				return s_instance;
			}
		}
		
		public static bool TryGetInstance(out T instance) => (instance = Instance) != null;
		
		[SuppressMessage("ReSharper", "StaticMemberInGenericType")]
		private static readonly HashSet<Type> s_instances = new();

		protected virtual void OnDisable()
		{
			if (s_instance != this) return;
			
			s_instance = null;
			s_instances.Remove(typeof(T));
		}

		protected virtual void OnEnable()
		{
			if (s_instance == null)
			{
				s_instance = this as T;
				s_instances.Add(typeof(T));
			}
			else
			{
				Destroy(this);
				Debug.LogWarning($"Singleton '{GetType().Name}' already exists, therefor this instance was destroyed.", gameObject);
			}
		}

		private static T s_instance;
	}
}
