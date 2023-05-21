using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metimos
{
	[Serializable]
	public class SerializableHashSet<T> : HashSet<T>, ISerializationCallbackReceiver
	{
		public SerializableHashSet()
		{
		}

		public SerializableHashSet(IEnumerable<T> collection)
		{
			foreach (T item in collection)
				Add(item);
		}

		[SerializeField] private List<T> _items = new();

		public void OnAfterDeserialize()
		{
			Clear();
			foreach (T item in _items)
				Add(item);
		}

		public void OnBeforeSerialize()
		{
			_items.Clear();
			foreach (T item in this)
				_items.Add(item);
		}
	}
}