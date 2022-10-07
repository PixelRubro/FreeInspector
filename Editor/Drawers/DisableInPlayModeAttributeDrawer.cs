using UnityEditor;
using UnityEngine;

namespace SoftBoiledGames.Inspector
{
    [CustomPropertyDrawer(typeof(DisableInPlayModeAttribute))]
    public class DisableInPlayModeAttributeDrawer : ConditionalAttributeDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        protected override PropertyDrawing GetPropertyDrawing()
        {
            return EditorApplication.isPlaying
                ? PropertyDrawing.Disable
                : PropertyDrawing.Enable;
        }
    }
}
