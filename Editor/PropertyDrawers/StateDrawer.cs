#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Metimos
{
	[CustomPropertyDrawer(typeof(State<>), true)]
	public class StateConditionDrawer : PropertyDrawer
	{
		private SerializedProperty _property;
		
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			_property ??= property.FindPropertyRelative("_value");

			return base.GetPropertyHeight(_property, label);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			_property ??= property.FindPropertyRelative("_value");

			EditorGUI.PropertyField(position, _property, label, true);
		}
	}
}

#endif
