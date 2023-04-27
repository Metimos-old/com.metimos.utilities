using UnityEngine;

namespace Metimos
{
	public sealed class Sphere : Shape
	{
		public Sphere(float radius)
		{
			this.radius = radius;
		}

		public readonly float radius;
		
		public override void Draw()
		{
			_material.SetPass(0);
			Graphics.DrawMeshNow(s_sphere, _matrix, 0);
		}

		protected override void UpdateMatrix()
		{
			_matrix = Matrix4x4.TRS(_position, _rotation, _scale * (radius * 2f));
		}
	}
}