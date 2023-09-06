using UnityEngine;
using UnityEditor;

namespace VermillionVanguard.Inspector
{
    [CustomPropertyDrawer(typeof(SeparatorAttribute))]
    public class SeparatorAttributeDrawer : BaseDecoratorDrawer 
    {
        public override float GetHeight()
        {
            var separatorAttribute = (SeparatorAttribute) attribute;
            return EditorGUIUtility.singleLineHeight + separatorAttribute.Thickness;
        }

        public override void OnGUI(Rect position)
        {
            Rect rect = EditorGUI.IndentedRect(position);
            rect.y += EditorGUIUtility.singleLineHeight / 3.0f;
            var separatorAttribute = (SeparatorAttribute) attribute;
            rect.height = separatorAttribute.Thickness;
            EditorGUI.DrawRect(rect, separatorAttribute.Color);
        }
    }
}