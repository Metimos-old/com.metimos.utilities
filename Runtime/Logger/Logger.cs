using System;
using UnityEngine;

namespace Metimos
{
	[Serializable]
	public class Logger : TextWrapper<DebugMode>
	{
		public DebugMode debugMode;
		
		public bool HasFlag(DebugMode flag) => debugMode.HasFlag(flag);
		
		protected override bool Condition(DebugMode value) => debugMode.HasFlag(value);
		protected override void Callback(string format, params object[] args) => Debug.LogFormat(format, args);
	}
}