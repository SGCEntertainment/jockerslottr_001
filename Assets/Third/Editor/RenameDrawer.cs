﻿using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Rename))]
public class RenameDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.PropertyField(position, property, new GUIContent((attribute as Rename).Name), true);
	}
}
