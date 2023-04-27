using UnityEngine;

namespace Metimos
{
	public sealed class Cube : Shape
	{
		public Cube(Vector3 size)
		{
			this.size = size;
		}

		public readonly Vector3 size;

		public override void Draw()
		{
			_material.SetPass(0);
			Graphics.DrawMeshNow(s_cube, _matrix, 0);
		}
		
		protected override void UpdateMatrix()
		{
			_matrix = Matrix4x4.TRS(_position, _rotation, Vector3.Scale(_scale, size));
		}
	}
}