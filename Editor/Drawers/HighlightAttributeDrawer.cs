using UnityEditor;
using UnityEngine;
using YoukaiFox.Inspector.Extensions;
using System.Linq;
using System.Reflection;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(HighlightAttribute))]
    public class HighlightAttributeDrawer : YoukaiPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var highlightAttribute = attribute as HighlightAttribute;
            var methodName = highlightAttribute.ValidationMethodName;
            var propertyName = highlightAttribute.TargetPropertyName;

            if (string.IsNullOrEmpty(propertyName))
            {
                if (!string.IsNullOrEmpty(methodName))
                {
                    HighlightUsingMethod(position, property, label);
                    return;
                }
            }
            else
            {
                HighlightUsingProperty(position, property, label);
                return;
            }

            HighlightField(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        private void HighlightUsingMethod(Rect position, SerializedProperty property, GUIContent label)
        {
            var highlightAttribute = attribute as HighlightAttribute;

            if (highlightAttribute.Value == null)
            {
                label.text = $"Couldn't find a method called {highlightAttribute.ValidationMethodName}.";
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            System.Object objectInstance = property.GetTargetObjectWithProperty();
            var targetMethod = objectInstance.GetAllMethods(m => m.Name.Equals(highlightAttribute.ValidationMethodName))
                .FirstOrDefault();

            if (targetMethod == null)
            {
                label.text = "You can't use a null value when using method validation.";
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var highlight = (bool) targetMethod.Invoke(
                property.serializedObject.targetObject, new [] { highlightAttribute.Value });

            if (highlight)
                HighlightField(position, property, label);
            else
                EditorGUI.PropertyField(position, property, label);
        }

        private void HighlightUsingProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            var highlightAttribute = attribute as HighlightAttribute;

            System.Object objectInstance = property.GetTargetObjectWithProperty();
            FieldInfo field = objectInstance.GetField(highlightAttribute.TargetPropertyName);
            PropertyInfo nonSerializedMember = objectInstance.GetProperty(highlightAttribute.TargetPropertyName);

            var objectValue = field != null ? field.GetValue(objectInstance) : nonSerializedMember.GetValue(objectInstance);

            if (!objectValue.ToBool(out bool memberValue))
            {
                label.text = $"Value {objectValue} is not a boolean";
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            Debug.LogError(memberValue);
            Debug.LogError((bool) objectValue);

            if (memberValue)
                HighlightField(position, property, label);
            else
                EditorGUI.PropertyField(position, property, label);
        }

        private void HighlightField(Rect position, SerializedProperty property, GUIContent label)
        {
            var highlightAttribute = attribute as HighlightAttribute;

            var padding = EditorGUIUtility.standardVerticalSpacing;
            var highlightRect = new Rect(position.x - padding, position.y - padding,
                position.width + (padding * 2), position.height + (padding * 2));

            EditorGUI.DrawRect(highlightRect, highlightAttribute.Color);

            var contentColor = GUI.contentColor;
            GUI.contentColor = Color.black;

            EditorGUI.PropertyField(position, property, label);
            GUI.contentColor = contentColor;
        }
    }
}
