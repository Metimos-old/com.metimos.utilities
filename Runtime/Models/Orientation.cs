using System.Runtime.InteropServices;
using UnityEngine;

namespace Metimos
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Orientation
	{
		public Orientation(Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}

		public Orientation(Transform transform)
		{
			position = transform.localPosition;
			rotation = transform.localRotation;
		}

		public Orientation(Transform transform, Space space)
		{
			position = space == Space.World ? transform.position : transform.localPosition;
			rotation = space == Space.World ? transform.rotation : transform.localRotation;
		}

		public Vector3 position;
		public Quaternion rotation;

		public static Orientation[] CreateArray(int size)
		{
			Orientation[] orientations = new Orientation[size];

			for (int i = 0; i < size; i++)
			{
				orientations[i] = new(Vector3.zero, Quaternion.identity);
			}

			return orientations;
		}

		public void ApplyTransform(Transform transform, Space space)
		{
			if (space == Space.World)
			{
				transform.position = position;
				transform.rotation = rotation;
			}
			else
			{
				transform.localPosition = position;
				transform.localRotation = rotation;
			}
		}

		public void CopyTransform(Transform transform)
		{
			position = transform.localPosition;
			rotation = transform.localRotation;
		}

		public void CopyTransform(Transform transform, Space space)
		{
			position = space == Space.World ? transform.position : transform.localPosition;
			rotation = space == Space.World ? transform.rotation : transform.localRotation;
		}

		public void MoveTowards(Orientation target, float linearDeltaTime, float angularDeltaTime)
		{
			position = Vector3.MoveTowards(position, target.position, linearDeltaTime);
			rotation = Quaternion.RotateTowards(rotation, target.rotation, angularDeltaTime);
		}
	}
}
