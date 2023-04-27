using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Metimos
{
	[Serializable]
	public sealed class InstancePool<T> : IDisposable, ISerializationCallbackReceiver where T : Component
	{
		public InstancePool(T prefab, int defaultCapacity = 10, int maxSize = 10000)
		{
			CreatePool();
			
			Prefab = prefab;
			DefaultCapacity = defaultCapacity;
			MaxSize = maxSize;
		}

		private ObjectPool<T> _pool;
		private HashSet<T> _activeInstances = new();

		[field: SerializeField] public T Prefab { get; private set; }
		[field: SerializeField] public int DefaultCapacity { get; private set; }
		[field: SerializeField] public int MaxSize { get; private set; }
		
		public int CountAll => _pool.CountAll;
		public int CountInactive => _pool.CountInactive;
		public int CountActive => _pool.CountActive;
		
		public IEnumerable<T> ActiveInstances => _activeInstances;
		
		public T Get() => _pool.Get();
		
		public void Release(T instance) => _pool.Release(instance);
		
		public void Clear() => _pool.Clear();
		
		public void Dispose() => _pool.Dispose();

		public void ReleaseAll()
		{
			foreach (T instance in _activeInstances)
				Release(instance);
		}
		
		public void OnBeforeSerialize()
		{
			_pool?.Dispose();
		}

		public void OnAfterDeserialize()
		{
			CreatePool();
		}
		
		private void CreatePool()
		{
			_pool = new ObjectPool<T>(OnCreate, OnGet, OnRelease, OnDestroy);
		}
		
		private T OnCreate()
		{
			T instance = Object.Instantiate(Prefab);
			instance.gameObject.SetActive(false);
			return instance;
		}
		
		private void OnRelease(T instance)
		{
			instance.gameObject.SetActive(false);
			_activeInstances.Remove(instance);
		}
		
		private void OnDestroy(T instance)
		{
			Object.Destroy(instance.gameObject);
			_activeInstances.Remove(instance);
		}
		
		private void OnGet(T instance)
		{
			instance.gameObject.SetActive(true);
			_activeInstances.Add(instance);
		}
	}
}