namespace Metimos
{
	public static class FloatExtensions
	{
		/// <summary>
		/// Wraps an angle in degrees to the range [-180, 180].
		/// </summary>
		/// <param name="angle">Angle in degrees.</param>
		/// <returns></returns>
		public static float ToShortArc(this float angle)
		{
			return angle switch
			{
				> 180f => angle - 360f,
				< -180f => angle + 360f,
				_ => angle,
			};
		}
		
		/// <summary>
		/// Wraps an angle in degrees to the range [0, 360].
		/// </summary>
		/// <param name="angle">Angle in degrees.</param>
		/// <returns></returns>
		public static float ToLongArc(this float angle)
		{
			return angle switch
			{
				> 360f => angle - 360f,
				< 0f => angle + 360f,
				_ => angle,
			};
		}
	}
}
