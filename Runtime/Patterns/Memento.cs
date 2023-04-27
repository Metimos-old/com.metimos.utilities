using System;
using UnityEngine;

namespace Metimos
{
	public class Memento<T>
	{
		public enum UpdateMode
		{
			Normal,
			Unscaled,
			System,
		}
		
		public Memento(T value, float duration, UpdateMode updateMode = UpdateMode.Normal)
		{
			defaultValue = value;
			Value = value;
			
			this.duration = duration;
			this.updateMode = updateMode;
		}

		protected readonly object _lock = new();
		protected T _value;
		protected double _lastUpdate;

		public readonly T defaultValue;
		public readonly UpdateMode updateMode;
		public float duration;

		public T DefaultValue => defaultValue;
		public bool IsExpired => GetTime() - _lastUpdate > duration;
		
		public T Value
		{
			get
			{
				lock (_lock)
				{
					return IsExpired ? defaultValue : _value;
				}
			}
			set
			{
				lock (_lock)
				{
					_value = value;
					
					if (value != null)
						_lastUpdate = GetTime();
				}
			}
		}

		public static implicit operator T(Memento<T> state) => state.Value;
		
		public override bool Equals(object obj)
		{
			lock (_value)
			{
				return obj != null && obj.Equals(_value);
			}
		}

		public override int GetHashCode()
		{
			lock (_lock)
			{
				return _value.GetHashCode();
			}
		}

		private double GetTime()
		{
			return updateMode switch
			{
				UpdateMode.Normal => Time.timeAsDouble,
				UpdateMode.Unscaled => Time.unscaledTimeAsDouble,
				UpdateMode.System => (DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds,
				_ => throw new ArgumentOutOfRangeException(),
			};
		}
	}
}