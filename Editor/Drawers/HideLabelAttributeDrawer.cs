using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(HideLabelAttribute))]
    public class HideLabelAttributeDrawer : YoukaiPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = "";
            EditorGUI.PropertyField(position, property, label);
        }
    }
}