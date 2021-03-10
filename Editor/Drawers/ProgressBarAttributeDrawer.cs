using UnityEngine;
using UnityEditor;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(ProgressBarAttribute))]
    public class ProgressBarAttributeDrawer: PropertyValidationDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            if (property.propertyType != SerializedPropertyType.Float)
            {
                GUI.Label(position, GetWarningMessage());
                return;
            }

            var progressAttribute = attribute as ProgressBarAttribute;

            if (progressAttribute == null)
                return;

            if (property.propertyType != SerializedPropertyType.Float)
                return;

            if ((progressAttribute.HideWhenZero) && (property.floatValue <= 0))
                return;

            var dynamicLabel = property.serializedObject.FindProperty(progressAttribute.Label);
            var barText = dynamicLabel == null ? property.name : dynamicLabel.stringValue;
            barText = ObjectNames.NicifyVariableName(barText);
            var progressValue = property.floatValue/1f;
            EditorGUI.ProgressBar(position, progressValue, barText);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var progressAttribute = attribute as ProgressBarAttribute;

            if (progressAttribute == null)
                return 0;

            if (property.propertyType != SerializedPropertyType.Float)
                return EditorGUIUtility.singleLineHeight;

            if ((progressAttribute.HideWhenZero) && (property.floatValue <= 0))
                return 0;

            return base.GetPropertyHeight(property, label);
        }

        public override string GetWarningMessage()
        {
            return "ERROR: the progress bar attribute can only be applied to float fields!";
        }
    }
}