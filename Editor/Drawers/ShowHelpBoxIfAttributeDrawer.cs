using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(ShowHelpBoxIfAttribute))]
    public class ShowHelpBoxIfAttributeDrawer : ConditionalHelpAttributeDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var targetAttribute = attribute as ShowHelpBoxIfAttribute;

            if (ComparedObjectValueIsTrue(property))
            {
                var marginEnd = position.yMin + StandardPropertySize + InspectorMargin;
                var upperRectPosition = new Vector2(position.xMin, position.yMin);
                var upperRectSize = new Vector2(position.size.x, GetHelpBoxHeight());
                var upperRect = new Rect(upperRectPosition, upperRectSize);

                var lowerRectPosition = new Vector2(position.xMin, upperRect.yMax);
                var lowerRectSize = new Vector2(position.size.x, position.size.y - GetHelpBoxHeight());
                var lowerRect = new Rect(lowerRectPosition, lowerRectSize);
                EditorGUI.HelpBox(upperRect, targetAttribute.HelpText, targetAttribute.MessageType);
                EditorGUI.PropertyField(lowerRect, property);
            }
            else
            {
                EditorGUI.PropertyField(position, property);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (ComparedObjectValueIsTrue(property))
                return GetFullHeight();

            return StandardPropertySize;
        }
    }
}