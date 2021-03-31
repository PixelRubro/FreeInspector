using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagAttributeDrawer : PropertyValidationDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.String)
            {
                label.text = GetWarningMessage();
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var tagAttribute = attribute as TagAttribute;
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
            EditorGUI.EndProperty();
        }
        public override string GetWarningMessage()
        {
            return "ERROR! Not a string field.";
        }
    }
}
