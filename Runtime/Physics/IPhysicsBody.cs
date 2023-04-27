using UnityEngine;

namespace Metimos
{
	public interface IPhysicsBody
	{
		public Vector3 Gravity { get; set; }

		public void OnBodyRotate(Quaternion delta);
		public void OnBodyMove(Vector3 delta);
	}
}