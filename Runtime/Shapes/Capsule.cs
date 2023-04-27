using UnityEngine;
using System.Collections.Generic;

namespace Metimos
{
	public sealed class Capsule : Shape
	{
		public Capsule(float radius, float height)
		{
			this.radius = radius;
			this.height = height;
			
			GenerateMesh();
		}

		public readonly float radius;
		public readonly float height;

		private Mesh _capsule;
		
		public override void Draw()
		{
			_material.SetPass(0);
			Graphics.DrawMeshNow(_capsule, _matrix, 0);
		}

		private void GenerateMesh()
		{
			// Get mesh info.
			List<Vector3> vertices = new();
			List<Vector3> normals = new();
			List<int> triangles = new();
			
			s_sphere.GetVertices(vertices);
			s_sphere.GetNormals(normals);
			s_sphere.GetTriangles(triangles, 0);
			
			// Stretch sphere into capsule.
			for (int i = 0; i < vertices.Count; i++)
			{
				Vector3 vertex = vertices[i] * (radius * 2f);
				Vector3 normal = normals[i];

				if (Vector3.Dot(normal, Vector3.up) > 0f)
					vertex += Vector3.up * (height / 2f - radius);
				else
					vertex -= Vector3.up * (height / 2f - radius);

				vertices[i] = vertex;
			}
			
			// Create mesh.
			_capsule = new()
			{
				name = "Capsule",
				vertices = vertices.ToArray(),
				normals = normals.ToArray(),
				triangles = triangles.ToArray(),
			};
		}
	}
}