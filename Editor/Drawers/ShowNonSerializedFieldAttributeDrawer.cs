using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(ShowNonSerializedFieldAttribute))]
    public class ShowNonSerializedFieldAttributeDrawer : YoukaiAttributeDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string fieldValue;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    // TODO show checkbox instead of boolean value
                    fieldValue = property.intValue.ToString();
                    break;
                case SerializedPropertyType.Boolean:
                    fieldValue = property.boolValue.ToString();
                    break;
                case SerializedPropertyType.Float:
                    fieldValue = property.floatValue.ToString("0.00000");
                    break;
                case SerializedPropertyType.String:
                    fieldValue = property.stringValue;
                    break;
                default:
                    if (property.objectReferenceValue)
                        fieldValue = property.objectReferenceValue.name;
                    else
                        // TODO inform type
                        fieldValue = "Type not supported!";
                    break;
            }

            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.LabelField(position, label.text, fieldValue);
            EditorGUI.EndDisabledGroup();
        }
    }
}