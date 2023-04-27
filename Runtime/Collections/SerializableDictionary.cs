using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metimos
{
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		public SerializableDictionary()
		{
		}

		public SerializableDictionary(Dictionary<TKey, TValue> dictionary)
		{
			foreach (KeyValuePair<TKey, TValue> pair in dictionary)
				Add(pair.Key, pair.Value);
		}

		[SerializeField] private List<TKey> _keys = new();
		[SerializeField] private List<TValue> _values = new();

		public void OnAfterDeserialize()
		{
			Clear();

			if (_keys.Count != _values.Count)
				return;

			for (int i = 0; i < _keys.Count; i++)
				Add(_keys[i], _values[i]);
		}

		public void OnBeforeSerialize()
		{
			_keys.Clear();
			_values.Clear();

			foreach (KeyValuePair<TKey, TValue> pair in this)
			{
				_keys.Add(pair.Key);
				_values.Add(pair.Value);
			}
		}
	}
}
