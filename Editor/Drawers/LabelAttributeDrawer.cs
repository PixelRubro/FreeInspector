using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeDrawer : YoukaiAttributeDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var labelAttribute = attribute as LabelAttribute;
            label.text = labelAttribute.OverriddenLabel;
            EditorGUI.PropertyField(position, property, label);
        }
    }
}