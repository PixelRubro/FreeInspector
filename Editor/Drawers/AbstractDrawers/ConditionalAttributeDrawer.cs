using UnityEditor;
using UnityEngine;
using System.Reflection;
using YoukaiFox.Inspector.Extensions;
using System.Runtime.CompilerServices;

namespace YoukaiFox.Inspector
{
    public abstract class ConditionalAttributeDrawer : YoukaiPropertyDrawer
    {
        public enum PropertyDrawing
        {
            Show, Hide, Disable, Enable
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            PropertyDrawing drawing = GetPropertyDrawing();

            if (IsComparisonValid(property))
            {
                switch (drawing)
                {
                    case PropertyDrawing.Show:
                        EditorGUI.PropertyField(position, property);
                        break;
                    case PropertyDrawing.Hide:
                        break;
                    case PropertyDrawing.Enable:
                        GUI.enabled = true;
                        EditorGUI.PropertyField(position, property);
                        break;
                    case PropertyDrawing.Disable:
                        GUI.enabled = false;
                        EditorGUI.PropertyField(position, property);
                        GUI.enabled = true;
                        break;
                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            }
            else
            {
                switch (drawing)
                {
                    case PropertyDrawing.Show:
                        break;
                    case PropertyDrawing.Hide:
                        EditorGUI.PropertyField(position, property);
                        break;
                    case PropertyDrawing.Enable:
                        GUI.enabled = false;
                        EditorGUI.PropertyField(position, property);
                        GUI.enabled = true;
                        break;
                    case PropertyDrawing.Disable:
                        GUI.enabled = true;
                        EditorGUI.PropertyField(position, property);
                        break;
                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsComparisonValid(property))
            {
                if (GetPropertyDrawing() == PropertyDrawing.Hide)
                    return 0f;
            }
            else
            {
                if (GetPropertyDrawing() == PropertyDrawing.Show)
                    return 0f;
            }

            return base.GetPropertyHeight(property, label);
        }

        protected abstract PropertyDrawing GetPropertyDrawing();

        private bool IsComparisonValid(SerializedProperty property)
        {
            System.Object objectInstance = property.GetTargetObjectWithProperty();
            var comparisonAttribute = attribute as ConditionalAttribute;
            FieldInfo field = objectInstance.GetField(comparisonAttribute.PropertyName);
            PropertyInfo nonSerializedMember = objectInstance.GetProperty(comparisonAttribute.PropertyName);

            var objectValue = field != null ? field.GetValue(objectInstance) : 
                                        nonSerializedMember.GetValue(objectInstance);

            if (!objectValue.ToBool(out bool memberValue))
                Debug.LogError($"Value {objectValue} is not a boolean");

            if (comparisonAttribute.TargetConditionValue == null)
                return memberValue;

            if (!comparisonAttribute.TargetConditionValue.ToBool(out bool targetConditionValue))
                Debug.LogError($"Value {comparisonAttribute.TargetConditionValue} is not a boolean");

            return memberValue == targetConditionValue;
        }
    }
}