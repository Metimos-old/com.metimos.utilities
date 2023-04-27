using System.Collections.Generic;
using UnityEngine;

namespace Metimos
{
	[AddComponentMenu("Metimos/Utilities/Mesh Collider Volume")]
	public sealed class MeshColliderVolume : MonoBehaviour
	{
		public Vector3Int size = new(10, 10, 1);
		public int subMesh;
		public int uvChannel;
		
		private void Start()
		{
			MeshCollider meshCollider = GetComponent<MeshCollider>();
			if (meshCollider == null) return;
			
			Mesh mesh = meshCollider.sharedMesh;
			IEnumerable<Mesh> subMeshes = mesh.Divide(size, subMesh, uvChannel);

			foreach (Mesh subMesh in subMeshes)
			{
				GameObject subObject = new("SubCollider");
				subObject.transform.SetParent(transform);
				subObject.transform.localPosition = Vector3.zero;
				subObject.transform.localRotation = Quaternion.identity;

				MeshCollider subCollider = subObject.AddComponent<MeshCollider>();
				subCollider.sharedMesh = subMesh;
			}
			
			Destroy(meshCollider);
			Destroy(this);
		}

		private void OnValidate()
		{
			size.x = Mathf.Max(1, size.x);
			size.y = Mathf.Max(1, size.y);
			size.z = Mathf.Max(1, size.z);
		}
	}
}
