using UnityEditor;
using UnityEngine;

namespace VermillionVanguard.Inspector
{
    [CustomPropertyDrawer(typeof(HideLabelAttribute))]
    public class HideLabelAttributeDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = string.Empty;
            EditorGUI.PropertyField(position, property, label);
        }
    }
}