using SiphoinUnityHelpers.XNodeExtensions.Attributes;
using System;
using UnityEditor;
using UnityEngine;

namespace SiphoinUnityHelpers.XNodeExtensions.Editor
{
    [CustomPropertyDrawer(typeof(NodeGuidAttribute))]
    public class NodeGuidPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string value = property.stringValue;

            GUIStyle style = EditorStyles.whiteLabel;

            style.fontStyle = FontStyle.Bold;

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("C", GUILayout.Width(24), GUILayout.Height(24)))
            {
                TextEditor textEditor = new TextEditor();

                textEditor.text = value;

                textEditor.SelectAll();

                textEditor.Copy();

                Debug.Log($"Guid {value} as copied to clipboard");
            }

            EditorGUILayout.EndHorizontal();

            EditorGUI.LabelField(position, $"{nameof(Guid)}: {value}", style);

        }
    }
}