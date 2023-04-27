using System;

namespace Metimos
{
	[Flags]
	[Serializable]
	public enum DebugMode
	{
		Off = 0,
		Logging = 2,
		Visualization = 4,
		Verbose = 8,
	}
}