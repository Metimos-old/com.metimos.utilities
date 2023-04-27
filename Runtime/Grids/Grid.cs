using System.Collections.Generic;
using UnityEngine;

namespace Metimos
{
	public class Grid
	{
		public Grid(float size)
		{
			Size = size;
			spacing = size / 2f;
		}
		
		public Grid(float size, float spacing)
		{
			Size = size;
			this.spacing = spacing;
		}
		
		public float spacing;

		private const int k_offset = ushort.MaxValue / 2 - 1;
		private const int k_bitOffset = 16;
		private const int k_bitMask = (1 << k_bitOffset) - 1;
		
		private float _size;
		private Vector2 _size2D;
		// private Vector2 _center2D;
		private Vector3 _size3D;
		private Vector3 _center3D;
		
		public float Size
		{
			get => _size;
			set
			{
				_size = value;
				
				_size2D = new(_size, _size);
				// _center2D = new(_size / 2f, _size / 2f);
				
				_size3D = new(_size, 0f, _size);
				_center3D = new(_size / 2f, 0f, _size / 2f);
			}
		}

		public HashSet<int> GetNearbyIndices(Vector2 position) => GetNearbyIndices(position.x, position.y);

		public HashSet<int> GetNearbyIndices(float x, float y)
		{
			float x0 = x + _size;
			float y0 = y + _size;
			
			HashSet<int> indices = new()
			{
				Encode(x0, y0),
				Encode(x0, y0 + spacing),
				Encode(x0, y0 - spacing),
				Encode(x0 - spacing, y0),
				Encode(x0 - spacing, y0 + spacing),
				Encode(x0 - spacing, y0 - spacing),
				Encode(x0 + spacing, y0),
				Encode(x0 + spacing, y0 + spacing),
				Encode(x0 + spacing, y0 - spacing),
			};

			return indices;
		}
		
		public Vector2[] GetNearbyGrids(Vector2 position) => GetNearbyGrids(position.x, position.y);

		public Vector2[] GetNearbyGrids(float x, float y)
		{
			HashSet<int> indices = GetNearbyIndices(x, y);
			Vector2[] grids = new Vector2[indices.Count];

			int count = 0;

			foreach (int index in indices)
				grids[count++] = Decode(index);
			
			return grids;
		}
		
		public Vector2 Decode(int index) =>
			new(
				(((index >> 0) & k_bitMask) - k_offset) * _size,
				(((index >> k_bitOffset) & k_bitMask) - k_offset) * _size
			);
		
		public Vector3 Decode3D(int index)
		{
			Vector3 position = Decode(index);
			return new(position.x, 0f, position.y);
		}
		
		public int Encode3D(Vector3 position) => Encode(position.x, position.z);
		public int Encode(Vector2 position) => Encode(position.x, position.y);

		public int Encode(float x, float y) =>
			(int)(x / _size + k_offset) << 0 |
			(int)(y / _size + k_offset) << k_bitOffset;

		public bool Contains(Vector2 center, Vector2 point) => (center - point).sqrMagnitude < _size * _size;
		public bool Contains(int index, Bounds bounds) => Contains(Decode(index), bounds);
		public bool Contains(Vector2 center, Bounds bounds) => Contains(center, bounds.min) && Contains(center, bounds.max);
		public bool Contains(int index, Vector2 position, float radius) => (Decode(index) - position).sqrMagnitude < (_size + radius) * (_size + radius);
		public bool Contains(Vector2 center, Vector2 position, float radius) => (center - position).sqrMagnitude < (_size + radius) * (_size + radius);
		
		public Rect GetRect(int index) => new(Decode(index), _size2D);
		public Rect GetRect(float x, float y) => GetRect(Encode(x, y));
		public Rect GetRect(Vector2 position) => GetRect(position.x, position.y);

		public Bounds GetBounds(int index) => new(Decode3D(index) - _center3D, _size3D);
		public Bounds GetBounds(float x, float y) => GetBounds(Encode(x, y));
		public Bounds GetBounds(Vector2 position) => GetBounds(position.x, position.y);
	}
}