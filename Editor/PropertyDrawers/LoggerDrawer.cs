#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Metimos
{
	[CustomPropertyDrawer(typeof(Logger), true)]
	public class LoggerDrawer : PropertyDrawer
	{
		private SerializedProperty _debugMode;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			_debugMode ??= property.FindPropertyRelative("debugMode");

			EditorGUI.PropertyField(position, _debugMode);
		}
	}
}

#endif
