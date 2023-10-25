using SiphoinUnityHelpers.XNodeExtensions.Math.Compute;
using UnityEditor;
using UnityEngine;

namespace SiphoinUnityHelpers.XNodeExtensions.Editor
{
    [CustomPropertyDrawer(typeof(ComputeType))]
    public class ComputeTypePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ComputeType computeType = (ComputeType)property.enumValueIndex;

            string[] enumNames = new string[]
            {
            "==",
            "!=",
            ">",
            "<",
            "<=",
            ">="
            };

            computeType = (ComputeType)EditorGUI.Popup(position, label.text, (int)computeType, enumNames);

            property.enumValueIndex = (int)computeType;
        }
    }
}