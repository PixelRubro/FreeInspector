using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PixelSparkStudio.Inspector
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagAttributeDrawer : BasePropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.String)
            {
                var message = "ERROR! Not a string field.";
                DrawErrorMessage(position, message);
                return;
            }

            var tagAttribute = attribute as TagAttribute;
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
            EditorGUI.EndProperty();
        }
    }
}
