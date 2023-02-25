using UnityEngine;
using UnityEditor;

namespace PixelSparkStudio.Inspector
{
    [CustomPropertyDrawer(typeof(ProgressBarAttribute))]
    public class ProgressBarAttributeDrawer: BasePropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            if (property.propertyType != SerializedPropertyType.Float)
            {
                var message = "ERROR: the progress bar attribute can only be applied to float fields!";
                DrawErrorMessage(position, message);
                return;
            }

            var progressAttribute = attribute as ProgressBarAttribute;

            if (progressAttribute == null)
                return;

            if ((progressAttribute.HideWhenZero) && (property.floatValue <= 0))
                return;

            var dynamicLabel = property.serializedObject.FindProperty(progressAttribute.Label);
            var barText = dynamicLabel == null ? property.name : dynamicLabel.stringValue;
            barText = ObjectNames.NicifyVariableName(barText);
            var progressValue = property.floatValue;
            EditorGUI.ProgressBar(position, progressValue, string.Empty);
            var shadowRect = new Rect(position.x, position.y - 2, position.width, position.height);
            EditorGUI.DropShadowLabel(shadowRect, barText);
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
    }
}