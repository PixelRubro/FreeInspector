using UnityEditor;
using UnityEngine;

namespace VermillionVanguard.Inspector
{
    [CustomPropertyDrawer(typeof(PasswordAttribute))]
    public class PasswordAttributeDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            property.stringValue = EditorGUI.PasswordField(position, GUIContent.none, property.stringValue);
            EditorGUI.EndProperty();
        }
    }
}
