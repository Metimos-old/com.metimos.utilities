using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Metimos
{
	[AddComponentMenu("Metimos/Utilities/Origin Shifting")]
	public class OriginShifting : MonoBehaviour
	{
		public static event Action<GameObject, Vector3> OnShift;
		
		public float threshold = 10f;
		
		/// <summary>
		/// Shifts the scene by an arbitrary amount.
		/// </summary>
		/// <param name="vector">Vector to transform the objects within the scene.</param>
		public void Shift(Vector3 vector)
		{
			Scene scene = gameObject.scene;
			
			foreach (GameObject root in scene.GetRootGameObjects())
				if (!root.isStatic)
					root.transform.position -= vector;
			
			OnShift?.Invoke(gameObject, vector);
		}

		protected virtual void FixedUpdate()
		{
			Vector3 position = transform.position;
			
			if (position.sqrMagnitude > threshold * threshold)
				Shift(position);
		}
	}
}
