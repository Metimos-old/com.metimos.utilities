using System;
using System.Reflection;
using UnityEngine;

namespace Metimos
{
	public static class AvatarExtensions
	{
		private const BindingFlags k_bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

		private static readonly Type s_avatarType = typeof(Avatar);
		private static readonly MethodInfo s_getAxisLength = s_avatarType.GetMethod("GetAxisLength", k_bindingFlags);
		private static readonly MethodInfo s_getPreRotation = s_avatarType.GetMethod("GetPreRotation", k_bindingFlags);
		private static readonly MethodInfo s_getPostRotation = s_avatarType.GetMethod("GetPostRotation", k_bindingFlags);
		private static readonly MethodInfo s_getZYPostQ = s_avatarType.GetMethod("GetZYPostQ", k_bindingFlags);
		private static readonly MethodInfo s_getZYRoll = s_avatarType.GetMethod("GetZYRoll", k_bindingFlags);
		private static readonly MethodInfo s_getLimitSign = s_avatarType.GetMethod("GetLimitSign", k_bindingFlags);

		public static float GetAxisLength(this Avatar avatar, HumanBodyBones humanBodyBone)
		{
			return (float)s_getAxisLength.Invoke(avatar, new object[] { (int)humanBodyBone });
		}

		public static Quaternion GetPreRotation(this Avatar avatar, HumanBodyBones humanBodyBone)
		{
			return (Quaternion)s_getPreRotation.Invoke(avatar, new object[] { (int)humanBodyBone });
		}

		public static Quaternion GetPostRotation(this Avatar avatar, HumanBodyBones humanBodyBone)
		{
			return (Quaternion)s_getPostRotation.Invoke(avatar, new object[] { (int)humanBodyBone });
		}

		public static Quaternion GetZYPostQ(this Avatar avatar, HumanBodyBones humanBodyBone, Quaternion parentRotation, Quaternion rotation)
		{
			return (Quaternion)s_getZYPostQ.Invoke(avatar, new object[] { (int)humanBodyBone, parentRotation, rotation });
		}

		public static Quaternion GetZYRoll(this Avatar avatar, HumanBodyBones humanBodyBone, Vector3 uvw)
		{
			return (Quaternion)s_getZYRoll.Invoke(avatar, new object[] { (int)humanBodyBone, uvw });
		}

		public static Vector3 GetLimitSign(this Avatar avatar, HumanBodyBones humanBodyBone)
		{
			return (Vector3)s_getLimitSign.Invoke(avatar, new object[] { (int)humanBodyBone });
		}

		public static Quaternion GetMuscleRotation(this Avatar avatar, HumanBodyBones bodyBone)
		{
			Quaternion preRotation = avatar.GetPreRotation(bodyBone);
			Quaternion postRotation = avatar.GetPostRotation(bodyBone);

			return GetMuscleRotation(preRotation, postRotation);
		}

		public static Quaternion GetMuscleRotation(Quaternion preRotation, Quaternion postRotation)
		{
			return preRotation * Quaternion.Inverse(postRotation);
		}
	}
}
