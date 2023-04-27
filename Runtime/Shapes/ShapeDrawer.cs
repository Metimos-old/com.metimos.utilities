using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metimos
{
	public sealed class ShapeDrawer : SingletonBehaviour<ShapeDrawer>
	{
		private const string k_shader = "Standard";

		private Shader _shader;
		private readonly List<Shape> _shapes = new();

		public static void Add(Shape shape, Color color, float duration = -1f)
		{
			if (shape == null) return;

			ShapeDrawer instance;
			
			if (!IsInstanced)
			{
				GameObject gameObject = new($"__{nameof(ShapeDrawer)}");
				DontDestroyOnLoad(gameObject);

				instance = gameObject.AddComponent<ShapeDrawer>();
				instance.InternalAdd(shape, color);
			}
			else
			{
				instance = Instance;
				Instance.InternalAdd(shape, color);
			}

			if (duration > 0f)
				instance.StartCoroutine(instance.RemoveDelayed(shape, duration));
		}
		
		public static void Remove(Shape shape)
		{
			if (IsInstanced)
				Instance.InternalRemove(shape);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			
			
			_shader = Shader.Find(k_shader);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			
			// Destroy materials.
			foreach (Shape shape in _shapes)
				if (shape.Material != null)
					DestroyImmediate(shape.Material);
			
			// Destroy object.
			if (gameObject != null)
				Destroy(gameObject);
		}
		
		private void OnRenderObject()
		{
			foreach (Shape shape in _shapes)
				if (!shape.hidden)
					shape.Draw();
		}

		private void InternalAdd(Shape shape, Color color)
		{
			shape.shader = _shader;
			shape.Color = color;

			// Add shape.
			_shapes.Add(shape);
		}

		private void InternalRemove(Shape shape)
		{
			_shapes.Remove(shape);
		}

		private IEnumerator RemoveDelayed(Shape shape, float time)
		{
			yield return new WaitForSeconds(time);
			
			InternalRemove(shape);
		}
	}
}