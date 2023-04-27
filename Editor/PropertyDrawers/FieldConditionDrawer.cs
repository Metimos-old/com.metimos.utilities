#if UNITY_EDITOR

using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Metimos
{
	[CustomPropertyDrawer(typeof(FieldCondition), true)]
	public class FieldConditionDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			(bool condition, bool visible) = CheckCondition(property);

			if (!visible && !condition)
				return 0f;

			return base.GetPropertyHeight(property, label);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			(bool condition, bool visible) = CheckCondition(property);

			if (!visible && !condition)
			{
				return;
			}

			EditorGUI.BeginDisabledGroup(!condition);

			EditorGUI.PropertyField(position, property, label, true);

			EditorGUI.EndDisabledGroup();
		}
		
		private const BindingFlags k_conditionFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static;
		private const BindingFlags k_fieldFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance;
		
		private static (bool value, bool visible) CheckCondition(SerializedProperty property)
		{
			object targetObject = property.serializedObject?.targetObject;

			if (targetObject == null)
				return (false, false);

			string[] paths = property.propertyPath.Split('.');

			if (paths.Length > 1)
			{
				for (int i = 0; i < paths.Length - 1; i++)
				{
					string path = paths[i];
					Type subTargetType = targetObject.GetType();
					FieldInfo subField = subTargetType.GetField(path, k_fieldFlags);

					targetObject = subField?.GetValue(targetObject);

					if (targetObject == null)
						return (false, false);
				}
			}

			Type targetType = targetObject.GetType();
			FieldInfo field = targetType.GetField(property.name, k_fieldFlags);

			if (field == null)
				return (true, true);

			if (field.GetCustomAttribute(typeof(FieldCondition)) is not FieldCondition fieldAttribute)
				return (false, false);

			PropertyInfo conditionProperty = targetType.GetProperty(fieldAttribute.name, k_conditionFlags);
			FieldInfo conditionField = targetType.GetField(fieldAttribute.name, k_fieldFlags);

			if (conditionProperty == null && conditionField == null)
				return (false, false);

			object conditionValue = conditionProperty == null ? conditionField.GetValue(targetObject) : conditionProperty.GetValue(targetObject, null);

			return fieldAttribute.negated ?
				(!conditionValue.Equals(fieldAttribute.value), fieldAttribute.visible) :
				(conditionValue.Equals(fieldAttribute.value), fieldAttribute.visible);
		}
	}
}

#endif
