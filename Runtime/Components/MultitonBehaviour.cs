using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Metimos
{
	public abstract class MultitonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static readonly HashSet<T> Instances = new();
		
		public static int Count => Instances.Count;

		// ReSharper disable once StaticMemberInGenericType
		private static readonly Random s_random = new();
		
		public static T GetRandomInstance() =>
			Instances.ElementAt(s_random.Next(0, Instances.Count));
		
		public static T GetClosest(Vector3 position) =>
			Instances.OrderBy(instance => (instance.transform.position - position).sqrMagnitude).First();
		
		public static T GetClosest(Vector3 position, Func<T, bool> predicate) =>
			Instances.Where(predicate).OrderBy(instance => (instance.transform.position - position).sqrMagnitude).First();
		
		protected virtual void OnEnable()
		{
			Instances.Add(this as T);
		}
		
		protected virtual void OnDisable()
		{
			Instances.Remove(this as T);
		}
	}
}