using UnityEngine;

namespace Metimos
{
	[RequireComponent(typeof(Rigidbody))]
	[AddComponentMenu("Metimos/Utilities/Physics Body")]
	public class PhysicsBody : MonoBehaviour, IPhysicsBody
	{
		public Vector3 Gravity { get; set; }
		public Rigidbody AttachedRigidbody { get; set; }

		protected Rigidbody _rigidbody;

		protected virtual void OnEnable()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_rigidbody.useGravity = false;
		}

		protected virtual void FixedUpdate()
		{
			if (!_rigidbody.IsSleeping() || _rigidbody.velocity.sqrMagnitude > Physics.sleepThreshold)
			{
				_rigidbody.AddForce(Gravity, ForceMode.Acceleration);
			}
		}

		public void OnBodyRotate(Quaternion delta) { }
		public void OnBodyMove(Vector3 delta) { }
	}
}
