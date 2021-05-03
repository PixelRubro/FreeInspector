using UnityEngine;
using UnityEditor;
using YoukaiFox.Inspector.Extensions;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace YoukaiFox.Inspector
{
    public abstract class YoukaiPropertyDrawer: PropertyDrawer 
    {
        private const string InputManagerPath = "ProjectSettings/InputManager.asset";
		private const string AxesPropertyName = "m_Axes";
		private const string NamePropertyName = "m_Name";

        public virtual float GetHelpBoxHeight()
		{
			return EditorGUIUtility.singleLineHeight * 2.0f;
		}

        protected MessageType ConvertMessageType(EMessageType eMessageType)
        {
            switch (eMessageType)
            {
                case EMessageType.None:
                    return MessageType.None;
                case EMessageType.Info:
                    return MessageType.Info;
                case EMessageType.Warning:
                    return MessageType.Warning;
                case EMessageType.Error:
                    return MessageType.Error;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }

        protected void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsHidden(property))
                return;

            var drawn = false;
            label = CheckForLabelAttributes(property, label);

            if (HasDisablingAttribute(property))
                EditorGUI.BeginDisabledGroup(true);

            if (property.GetAttribute<LeftToggleAttribute>() != null)
            {
                DrawFieldWithToggleOnTheLeft(position, property, label);
                drawn = true;
            }

            if (property.GetAttribute<InputAttribute>() != null)
            {
                DrawInputField(position, property, label);
                drawn = true;
            }

            if (property.GetAttribute<EnumFlagsAttribute>() != null)
            {
                DrawEnumFlagsField(position, property, label);
                drawn = true;
            }

            if ((!drawn))
                DrawPropertySimple(position, property, label);

            if (HasDisablingAttribute(property))
                EditorGUI.EndDisabledGroup();
        }

        protected void DrawErrorMessage(Rect position, string errorMessage)
        {
            var padding = EditorGUIUtility.standardVerticalSpacing;

            var highlightRect = new Rect(position.x - padding, position.y - padding,
                position.width + (padding * 2), position.height + (padding * 2));

            EditorGUI.DrawRect(highlightRect, Color.red);

            var contentColor = GUI.contentColor;
            GUI.contentColor = Color.white;
            EditorGUI.LabelField(position, errorMessage);
            GUI.contentColor = contentColor;
        }

        // Credits: https://github.com/dbrizov/NaughtyAttributes
		/// <summary>
		/// Creates a dropdown
		/// </summary>
		/// <param name="rect">The rect the defines the position and size of the dropdown in the inspector</param>
		/// <param name="serializedObject">The serialized object that is being updated</param>
		/// <param name="target">The target object that contains the dropdown</param>
		/// <param name="dropdownField">The field of the target object that holds the currently selected dropdown value</param>
		/// <param name="label">The label of the dropdown</param>
		/// <param name="selectedValueIndex">The index of the value from the values array</param>
		/// <param name="values">The values of the dropdown</param>
		/// <param name="displayOptions">The display options for the values</param>
		protected void DrawDropdown(
			Rect rect, SerializedObject serializedObject, object target, FieldInfo dropdownField,
			string label, int selectedValueIndex, object[] values, string[] displayOptions)
		{
			EditorGUI.BeginChangeCheck();

			int newIndex = EditorGUI.Popup(rect, label, selectedValueIndex, displayOptions);

			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(serializedObject.targetObject, "Dropdown");
				dropdownField.SetValue(target, values[newIndex]);
			}
		}

        protected void DrawFieldWithToggleOnTheLeft(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Boolean)
            {
                var message = "ERROR! Not a boolean field.";
                DrawErrorMessage(position, message);
                return;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            var value = EditorGUI.ToggleLeft(position, label, property.boolValue);

            if (EditorGUI.EndChangeCheck())
                property.boolValue = value;

            EditorGUI.EndProperty();
        }

        protected void DrawInputField(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                var message = "ERROR! Not a string field.";
                DrawErrorMessage(position, message);
                return;
            }

            var inputManagerAsset = AssetDatabase.LoadAssetAtPath(InputManagerPath, typeof(object));
            var serializedInputManager = new SerializedObject(inputManagerAsset);
            var axesProperty = serializedInputManager.FindProperty(AxesPropertyName);
            var axesSet = new HashSet<string>();
            axesSet.Add("<none>");

            for (int i = 0; i < axesProperty.arraySize; i++)
            {
                axesSet.Add(axesProperty.GetArrayElementAtIndex(i).FindPropertyRelative(NamePropertyName).stringValue);
            }

            var axesArray = axesSet.ToArray();
            var propertyStringValue = property.stringValue;
            int popupSelectionIndex = 0;

            for (int i = 1; i < axesArray.Length; i++)
            {
                if (axesArray[i].Equals(propertyStringValue))
                {
                    popupSelectionIndex = i;
                    break;
                }
            }

            var selectedOptionIndex = EditorGUI.Popup(position, label.text, popupSelectionIndex, axesArray);

            if (selectedOptionIndex > 0)
                property.stringValue = axesArray[selectedOptionIndex];
            else
                property.stringValue = string.Empty;
        }

        protected Texture2D FindIcon(string iconPath)
        {
            var path = Path.Combine(Application.dataPath, iconPath);
            var image = File.ReadAllBytes(path);

            if (image == null)
                return null;

            var iconTexture = new Texture2D(1, 1);
            iconTexture.LoadImage(image);
            iconTexture.name = "PrefabIcon";
            iconTexture.Apply();
            return iconTexture;
        }

        protected void DrawPropertySimple(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, property.isExpanded);
        }

        protected void DrawEnumFlagsField(Rect position, SerializedProperty property, GUIContent label)
        {
            Enum targetEnum = property.GetTargetObjectOfProperty() as Enum;

            if (targetEnum == null)
            {
                var message = "ERROR! EnumFlags attribute can only be used on enums.";
                DrawErrorMessage(position, message);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            Enum updatedEnum = EditorGUI.EnumFlagsField(position, label.text, targetEnum);
            property.intValue = (int) Convert.ChangeType(updatedEnum, targetEnum.GetType());
            EditorGUI.EndProperty();
            property.serializedObject.ApplyModifiedProperties();
        }

        private bool HasDisablingAttribute(SerializedProperty property)
        {
            var readonlyAttribute = property.GetAttribute<ReadOnlyAttribute>();

            if (readonlyAttribute != null)
                return true;

            var disablePlayModeAttribute = property.GetAttribute<DisableInPlayModeAttribute>();

            if ((disablePlayModeAttribute != null) && (Application.isPlaying))
                return true;
                
            var playmodeOnlyAttribute = property.GetAttribute<PlayModeOnlyAttribute>();

            if ((playmodeOnlyAttribute != null) && (!Application.isPlaying))
                return true;
                
            return false;
        }

        private bool IsHidden(SerializedProperty property)
        {
            var showInPlayModeAttribute = property.GetAttribute<ShowInPlayModeAttribute>();

            if ((showInPlayModeAttribute != null) && (!Application.isPlaying))
                return true;

            var hideInPlayModeAttribute = property.GetAttribute<HideInPlayModeAttribute>();

            if ((hideInPlayModeAttribute != null) && (Application.isPlaying))
                return true;

            return false;
        }

        private GUIContent CheckForLabelAttributes(SerializedProperty property, GUIContent label)
        {
            var labelAttribute = property.GetAttribute<LabelAttribute>();

            if (labelAttribute != null)
                label.text = labelAttribute.OverriddenLabel;

            var iconAttribute = property.GetAttribute<IconAttribute>();

            if ((iconAttribute != null) && (!string.IsNullOrEmpty(iconAttribute.IconPath)))
            {
                var icon = FindIcon(iconAttribute.IconPath);
                label.image = icon;
            }

            return label;
        }
    }
}