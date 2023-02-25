using UnityEditor;
using UnityEngine;

namespace PixelSparkStudio.Inspector
{
    [CustomPropertyDrawer(typeof(EnableInPlayModeAttribute))]
    public class EnableInPlayModeAttributeDrawer : ConditionalAttributeDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        protected override PropertyDrawing GetPropertyDrawing()
        {
            return EditorApplication.isPlaying 
                ? PropertyDrawing.Enable 
                : PropertyDrawing.Disable;
        }
    }
}
