using SiphoinUnityHelpers.XNodeExtensions.Math.Compute;
using UnityEditor;
using UnityEngine;

namespace SiphoinUnityHelpers.XNodeExtensionss.Editor
{
    [CustomPropertyDrawer(typeof(ComputeObjectsType))]
    public class ComputeTypeObjectsPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ComputeObjectsType computeType = (ComputeObjectsType)property.enumValueIndex;

            string[] enumNames = new string[]
            {
            "==",
            "!="
            };

            computeType = (ComputeObjectsType)EditorGUI.Popup(position, label.text, (int)computeType, enumNames);

            property.enumValueIndex = (int)computeType;
        }
    }
}
