using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(InputAttribute))]
    public class InputAttributeDrawer : YoukaiPropertyDrawer 
    {
        private const string InputManagerPath = "ProjectSettings/InputManager.asset";
		private const string AxesPropertyName = "m_Axes";
		private const string NamePropertyName = "m_Name";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
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
    }
}