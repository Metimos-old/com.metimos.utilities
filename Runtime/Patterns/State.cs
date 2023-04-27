using System;
using System.Threading;
using UnityEngine;

namespace Metimos
{
	[Serializable]
	public class State<T>
	{
		public State(T value = default)
		{
			_value = value;
		}

		[SerializeField] private T _value;
		private readonly Mutex _mutex = new();

		public event Action<T, T> OnChanged;

		public T Value
		{
			get
			{
				try
				{
					_mutex.WaitOne();
					
					return _value;
				}
				finally
				{
					_mutex.ReleaseMutex();
				}
			}

			set
			{
				try
				{
					_mutex.WaitOne();
					
					if (_value.Equals(value)) return;

					OnChanged?.Invoke(value, _value);

					_value = value;
				}
				finally
				{
					_mutex.ReleaseMutex();
				}
			}
		}

		public static explicit operator State<T>(T value) => new(value);

		public static implicit operator T(State<T> state) => state.Value;

		public static Delegate operator +(State<T> state, Delegate method)
		{
			return Delegate.Combine(state.OnChanged, method);
		}

		public override bool Equals(object obj)
		{
			lock (_value)
			{
				return obj != null && obj.Equals(_value);
			}
		}

		public override int GetHashCode()
		{
			try
			{
				_mutex.WaitOne();
				
				return _value.GetHashCode();
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
		}
	}
}
