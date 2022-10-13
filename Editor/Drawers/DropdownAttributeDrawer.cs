using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using SoftBoiledGames.Inspector.Extensions;

namespace SoftBoiledGames.Inspector
{
    [CustomPropertyDrawer(typeof(DropdownAttribute))]
    public class DropdownAttributeDrawer : BasePropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var dropdownAttribute = attribute as DropdownAttribute;
            object values = GetValues(property, dropdownAttribute.ValuesCollectionName);
            var objectInstance = property.GetTargetObjectWithProperty();
            FieldInfo fieldInfo = objectInstance.GetField(property.name);

            float propertyHeight = AreValuesValid(values, fieldInfo)
                ? base.GetPropertyHeight(property, label)
                : GetPropertyHeight(property, label) + GetHelpBoxHeight();

            return propertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var dropdownAttribute = attribute as DropdownAttribute;
            object target = property.GetTargetObjectWithProperty();

            object valuesObject = GetValues(property, dropdownAttribute.ValuesCollectionName);
            FieldInfo dropdownField = target.GetField(property.name);

            if (AreValuesValid(valuesObject, dropdownField))
            {
                if (valuesObject is IList && dropdownField.FieldType == GetElementType(valuesObject))
                {
                    // Selected value
                    object selectedValue = dropdownField.GetValue(target);

                    // Values and display options
                    IList valuesList = (IList)valuesObject;
                    object[] values = new object[valuesList.Count];
                    string[] displayOptions = new string[valuesList.Count];

                    for (int i = 0; i < values.Length; i++)
                    {
                        object value = valuesList[i];
                        values[i] = value;
                        displayOptions[i] = value == null ? "<null>" : value.ToString();
                    }

                    // Selected value index
                    int selectedValueIndex = Array.IndexOf(values, selectedValue);
                    
                    if (selectedValueIndex < 0)
                        selectedValueIndex = 0;

                    DrawDropdown(
                        position, property.serializedObject, target, dropdownField, label.text,
                        selectedValueIndex, values, displayOptions);
                }
                else if (valuesObject is IDropdownList)
                {
                    // Current value
                    object selectedValue = dropdownField.GetValue(target);

                    // Current value index, values and display options
                    int index = -1;
                    int selectedValueIndex = -1;
                    List<object> values = new List<object>();
                    List<string> displayOptions = new List<string>();
                    IDropdownList dropdown = (IDropdownList)valuesObject;

                    using (IEnumerator<KeyValuePair<string, object>> dropdownEnumerator = dropdown.GetEnumerator())
                    {
                        while (dropdownEnumerator.MoveNext())
                        {
                            index++;
                            KeyValuePair<string, object> current = dropdownEnumerator.Current;

                            if (current.Value?.Equals(selectedValue) == true)
                                selectedValueIndex = index;

                            values.Add(current.Value);

                            if (current.Key == null)
                                displayOptions.Add("<null>");
                            else if (string.IsNullOrWhiteSpace(current.Key))
                                displayOptions.Add("<empty>");
                            else
                                displayOptions.Add(current.Key);
                        }
                    }

                    if (selectedValueIndex < 0)
                        selectedValueIndex = 0;

                    DrawDropdown(
                        position, property.serializedObject, target, dropdownField, label.text,
                        selectedValueIndex, values.ToArray(), displayOptions.ToArray());
                }
            }
            else
            {
                var message = $"ERROR! Invalid values {dropdownAttribute.ValuesCollectionName} {dropdownAttribute.GetType().Name}";
                DrawErrorMessage(position, message);
            }

            EditorGUI.EndProperty();
        }

        private object GetValues(SerializedProperty property, string valuesName)
        {
            object target = property.GetTargetObjectWithProperty();

            FieldInfo valuesFieldInfo = target.GetField(valuesName);
            if (valuesFieldInfo != null)
            {
                return valuesFieldInfo.GetValue(target);
            }

            PropertyInfo valuesPropertyInfo = target.GetProperty(valuesName);
            if (valuesPropertyInfo != null)
            {
                return valuesPropertyInfo.GetValue(target);
            }

            MethodInfo methodValuesInfo = target.GetMethod(valuesName);
            if (methodValuesInfo != null &&
                methodValuesInfo.ReturnType != typeof(void) &&
                methodValuesInfo.GetParameters().Length == 0)
            {
                return methodValuesInfo.Invoke(target, null);
            }

            return null;
        }

        private bool AreValuesValid(object values, FieldInfo dropdownField)
        {
            if (values == null || dropdownField == null)
            {
                return false;
            }

            if ((values is IList && dropdownField.FieldType == GetElementType(values)) ||
                (values is IDropdownList))
            {
                return true;
            }

            return false;
        }

        private Type GetElementType(object values)
        {
            Type valuesType = values.GetType();
            Type elementType = valuesType.GetListElementType();
            return elementType;
        }
    }
}