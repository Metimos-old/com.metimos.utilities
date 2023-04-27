using System;
using UnityEngine;

namespace Metimos
{
	[AttributeUsage(AttributeTargets.Field)]
	public class FieldCondition : PropertyAttribute
	{
		public FieldCondition(string name, object value, bool visible = true, bool negated = false)
		{
			this.name = name;
			this.value = value;
			this.visible = visible;
			this.negated = negated;
		}

		public readonly string name;
		public readonly object value;
		public readonly bool visible;
		public readonly bool negated;
	}
}
