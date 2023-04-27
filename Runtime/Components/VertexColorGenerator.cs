using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Metimos
{
	[AddComponentMenu("Metimos/Utilities/Vertex Color Generator")]
	public class VertexColorGenerator : MonoBehaviour
	{
		private struct VertexColorJob : IJobParallelFor
		{
			[ReadOnly] public NativeArray<Vector3> vertices;
			[ReadOnly] public NativeArray<Vector3> normals;
			[ReadOnly] public NativeArray<float> offsets;
			public int octaves;
			public float frequency;
			public float persistence;
			public float lacunarity;
			public float power;
			public Color weights;
			public Matrix4x4 matrix;
			public LayerMode redMode;
			public LayerMode blueMode;
			public LayerMode greenMode;
			public NativeArray<Color> colors;

			public void Execute(int index)
			{
				Vector3 vertex = matrix * vertices[index];
				// Vector3 normal = normals[index];

				float[] values = new float[3];
				
				for (int i = 0; i < 3; i++)
				{
					int j = i * 4;
					float x = GetValue(vertex.x + offsets[j + 0], vertex.y + offsets[j + 1]);
					float y = GetValue(vertex.y + offsets[j + 2], vertex.z + offsets[j + 3]);
					float z = GetValue(vertex.z + offsets[j + 4], vertex.x + offsets[j + 5]);
					
					values[i] = Overlay(Overlay(x, y), z);
				}
				
				colors[index] = new(
					values[0] * weights.r,
					values[1] * weights.g,
					values[2] * weights.b,
					1f
				);
			}

			private static float Overlay(float a, float b)
			{
				return a > 0.5f ? 1f - (1f - 2f * (a - 0.5f)) * (1f - b) : 2f * a * b;
			}

			private float GetValue(float u, float v)
			{
				float frequency = this.frequency;
				float amplitude = 1f;
				float value = 0f;
				float sum = 0f;

				for (int o = 0; o < octaves; o++)
				{
					value += Mathf.PerlinNoise(u * frequency, v * frequency) * amplitude;
					sum += amplitude;

					frequency *= lacunarity;
					amplitude *= persistence;
				}

				return Mathf.Clamp01(Mathf.Pow(value / sum, power));
			}
		}

		public enum LayerMode
		{
			Normal,
			Crease,
		}

		public int seed;
		public float frequency = 1f;
		public float lacunarity = 2f;
		public float persistence = 0.5f;
		[Range(1, 8)] public int octaves = 3;
		public float power = 1f;
		[ColorUsage(false, false)] public Color weights = Color.white;
		// public LayerMode redMode = LayerMode.Normal;
		// public LayerMode blueMode = LayerMode.Normal;
		// public LayerMode greenMode = LayerMode.Normal;
		public MeshFilter[] meshFilters;

		private const float k_offsetRange = 1000f;
		private float[] _offsets;
		private List<GameObject> _staticObjects = new();

		private async void Awake()
		{
			await GenerateAll();
		}

		[ContextMenu("Vertex Colors: Reset")]
		private void ResetColors()
		{
			foreach (MeshFilter meshFilter in meshFilters)
				ResetColors(meshFilter);
		}

		[ContextMenu("Vertex Colors: Generate")]
		private async Task GenerateAll()
		{
			// Cache static transforms.
			_staticObjects = new(GetComponentsInChildren<Transform>().ToList().ConvertAll(transform => transform.gameObject).Where(gameObject => gameObject.isStatic));
			_staticObjects.ForEach(gameObject => gameObject.isStatic = false);

			// Update offsets.
			UpdateOffsets();

			// Generate colors for each mesh filter.
			foreach (MeshFilter meshFilter in meshFilters)
				await GenerateColors(meshFilter);

			// Reapply static transforms.
			if (_staticObjects.Count > 0)
			{
				if (Application.isPlaying)
					StaticBatchingUtility.Combine(_staticObjects.ToArray(), gameObject);

				_staticObjects.ForEach(gameObject => gameObject.isStatic = true);
				_staticObjects.Clear();
			}

			// Dirty object.
			#if UNITY_EDITOR
			EditorUtility.SetDirty(this);
			#endif
		}

		[ContextMenu("Find Filters")]
		private void FindFilters()
		{
			meshFilters = GetComponentsInChildren<MeshFilter>();

			#if UNITY_EDITOR
			EditorUtility.SetDirty(this);
			#endif
		}

		private void UpdateOffsets()
		{
			// Generate offsets.
			System.Random random = new(seed == 0 ? DateTime.Now.Millisecond : seed);

			_offsets = new float[100];

			for (int i = 0; i < 100; i++)
				_offsets[i] = (float)random.NextDouble() * k_offsetRange;
		}

		public static void ResetColors(MeshFilter filter)
		{
			Mesh mesh = filter.sharedMesh;
			int vertexCount = mesh.vertexCount;
			
			Color[] colors = new Color[vertexCount];
			for (int i = 0; i < vertexCount; i++)
				colors[i] = Color.white;
			
			mesh.SetColors(colors);
		}

		public async Task GenerateColors(MeshFilter filter)
		{
			Mesh mesh = filter.sharedMesh;
			int vertexCount = mesh.vertexCount;
			Transform transform = filter.transform;

			// Create native arrays.
			NativeArray<Vector3> vertices = new(mesh.vertices, Allocator.TempJob);
			NativeArray<Vector3> normals = new(mesh.normals, Allocator.TempJob);
			NativeArray<Color> colors = new(vertexCount, Allocator.TempJob);
			NativeArray<float> offsets = new(_offsets, Allocator.TempJob);

			// Create job.
			VertexColorJob job = new()
			{
				matrix = transform.localToWorldMatrix,
				vertices = vertices,
				normals = normals,
				colors = colors,

				frequency = frequency,
				lacunarity = lacunarity,
				octaves = octaves,
				offsets = offsets,
				persistence = persistence,
				power = power,
				weights = weights,
			};

			// Run job.
			JobHandle handle = job.Schedule(vertexCount, 16);

			while (!handle.IsCompleted)
			{
				await Task.Yield();
			}

			// Set colors.
			handle.Complete();
			mesh.SetColors(job.colors);
			
			// Dispose native arrays.
			vertices.Dispose();
			normals.Dispose();
			colors.Dispose();
			offsets.Dispose();
		}
	}
}