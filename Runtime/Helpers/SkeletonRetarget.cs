using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Metimos
{
	public static class SkeletonRetarget
	{
		public static void ApplyTargets(IEnumerable<SkinnedMeshRenderer> renderers, Transform source, Transform target, ref Dictionary<Transform, Transform> targets)
		{
			foreach (SkinnedMeshRenderer renderer in renderers)
				ApplyTargets(renderer, source, target, ref targets);
		}

		public static void ApplyTargets(SkinnedMeshRenderer renderer, Transform source, Transform target, ref Dictionary<Transform, Transform> targets)
		{
			// Cache transforms.
			Dictionary<Transform, int> indexes = new();
			int boneCount = renderer.bones.Length;

			for (int i = 0; i < boneCount; i++)
			{
				Transform bone = renderer.bones[i];
				indexes.Add(bone, i);
			}

			Transform[] bones = new Transform[boneCount];

			// Make sure cache exists.
			targets ??= new();

			// Update bone targets recursively.
			Branch(source, target, ref indexes, ref bones, ref targets);

			// Update renderer.
			renderer.rootBone = target;
			renderer.bones = bones;
		}

		public static Transform DuplicateSkeleton(Transform root, SkinnedMeshRenderer[] renderers, ref Dictionary<Transform, Transform> targets)
		{
			// Create a new root for skinned renderers.
			Transform newRoot = new GameObject($"{root.name} (Clone)").transform;
			newRoot.SetParent(root.parent);

			// Duplicate children.
			foreach (Transform child in root)
			{
				Transform newChild = Object.Instantiate(child, newRoot);
				newChild.name = child.name;
			}

			// Move bone targets to new root.
			ApplyTargets(renderers, root, newRoot, ref targets);

			// Remove components.
			Component[] components = root.GetComponentsInChildren<Component>().Where(component => component is not Transform).ToArray();

			foreach (Component component in components)
				Object.Destroy(component);

			// Return new root.
			return newRoot;
		}

		private static void Branch(
			Transform source,
			Transform target,
			ref Dictionary<Transform, int> indexes,
			ref Transform[] bones,
			ref Dictionary<Transform, Transform> targets
		)
		{
			if (source == null || target == null)
				return;

			if (indexes.TryGetValue(source, out int boneIndex))
			{
				bones[boneIndex] = target;

				if (!targets.ContainsKey(source))
					targets.Add(source, target);
			}

			for (int i = 0; i < source.childCount; i++)
				Branch(source.GetChild(i), target.GetChild(i), ref indexes, ref bones, ref targets);
		}
	}
}
