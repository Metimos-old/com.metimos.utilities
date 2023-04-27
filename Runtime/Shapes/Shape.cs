using UnityEngine;
using System.Collections.Generic;

namespace Metimos
{
	public abstract class Shape
	{
		protected Shape()
		{
			if (!s_didInit)
				Init();
		}
		
		public Shader shader;
		public bool hidden;

		public Vector3 Position
		{
			get => _position;
			set
			{
				_position = value;
				UpdateMatrix();
			}
		}
		
		public Quaternion Rotation
		{
			get => _rotation;
			set
			{
				_rotation = value;
				UpdateMatrix();
			}
		}
		
		public Vector3 Scale
		{
			get => _scale;
			set
			{
				_scale = value;
				UpdateMatrix();
			}
		}
		
		public Color Color
		{
			get => _color;
			set
			{
				_color = value;
				
				int colorId = GetColorId(value);
				if (s_materials.TryGetValue(colorId, out _material)) return;

				_material = new(shader)
				{
					name = $"Shape-Color{colorId}",
					hideFlags = HideFlags.DontSave,
				};
				
				_material.SetFade();
				_material.SetColor(k_colorId, value);
				
				s_materials.Add(colorId, _material);
			}
		}

		public Material Material => _material;
		
		protected static readonly Dictionary<int, Material> s_materials = new();
		protected static bool s_didInit;
		protected static Mesh s_cube;
		protected static Mesh s_cylinder;
		protected static Mesh s_sphere;

		protected Material _material;
		protected Color _color;
		protected Matrix4x4 _matrix;
		protected Vector3 _position;
		protected Quaternion _rotation = Quaternion.identity;
		protected Vector3 _scale = Vector3.one;

		private static readonly int k_colorId = Shader.PropertyToID("_Color");

		public abstract void Draw();

		protected static Mesh GetMesh(PrimitiveType primitiveType)
		{
			GameObject proxy = GameObject.CreatePrimitive(primitiveType);
			Mesh mesh = proxy.GetComponent<MeshFilter>().sharedMesh;
			
			Object.DestroyImmediate(proxy);

			return mesh;
		}
		
		protected virtual void UpdateMatrix()
		{
			_matrix = Matrix4x4.TRS(_position, _rotation, _scale);
		}

		private static void Init()
		{
			s_didInit = true;
			
			s_cube = GetMesh(PrimitiveType.Cube);
			s_cylinder = GetMesh(PrimitiveType.Cylinder);
			s_sphere = GetMesh(PrimitiveType.Sphere);
		}

		private static int GetColorId(Color color)
		{
			return
				(byte)(color.r * 255f) << 0 |
				(byte)(color.g * 255f) << 4 |
				(byte)(color.b * 255f) << 8;
		}
	}
}