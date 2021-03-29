using UnityEditor;
using UnityEngine;
using System.Reflection;
using YoukaiFox.Inspector.Extensions;
using System;
using System.Runtime.CompilerServices;

namespace YoukaiFox.Inspector
{
    public abstract class ConditionalHelpAttributeDrawer : YoukaiPropertyDrawer
    {
        public const int MessageTextPadding = 44;
        public const int InspectorMargin = 4;
        public const int StandardPropertySize = 16;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var conditionalHelpAttribute = attribute as ConditionalHelpAttribute;

            if (ShowHelpOnValidComparison())
            {
                if (ComparedObjectValueIsTrue(property))
                {
                    var marginEnd = position.yMin + StandardPropertySize + InspectorMargin;
                    var upperRectPosition = new Vector2(position.xMin, position.yMin);
                    var upperRectSize = new Vector2(position.size.x, GetHelpBoxHeight());
                    var upperRect = new Rect(upperRectPosition, upperRectSize);

                    var lowerRectPosition = new Vector2(position.xMin, upperRect.yMax);
                    var lowerRectSize = new Vector2(position.size.x, position.size.y - GetHelpBoxHeight());
                    var lowerRect = new Rect(lowerRectPosition, lowerRectSize);
                    EditorGUI.HelpBox(upperRect, conditionalHelpAttribute.HelpText, ConvertMessageType(conditionalHelpAttribute.MessageType));
                    DrawProperty(position, property, label);
                    // EditorGUI.PropertyField(lowerRect, property);
                }
                else
                {
                    DrawProperty(position, property, label);
                    // EditorGUI.PropertyField(position, property);
                }
            }
            else
            {
                if (ComparedObjectValueIsTrue(property))
                {
                    DrawProperty(position, property, label);
                }
                else
                {
                    var marginEnd = position.yMin + StandardPropertySize + InspectorMargin;
                    var upperRectPosition = new Vector2(position.xMin, position.yMin);
                    var upperRectSize = new Vector2(position.size.x, GetHelpBoxHeight());
                    var upperRect = new Rect(upperRectPosition, upperRectSize);

                    var lowerRectPosition = new Vector2(position.xMin, upperRect.yMax);
                    var lowerRectSize = new Vector2(position.size.x, position.size.y - GetHelpBoxHeight());
                    var lowerRect = new Rect(lowerRectPosition, lowerRectSize);
                    EditorGUI.HelpBox(upperRect, conditionalHelpAttribute.HelpText, ConvertMessageType(conditionalHelpAttribute.MessageType));
                    DrawProperty(position, property, label);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (ComparedObjectValueIsTrue(property))
            {
                if (ShowHelpOnValidComparison())
                {
                    return GetFullHeight();
                }
            }
            else
            {
                if (!ShowHelpOnValidComparison())
                {
                    return GetFullHeight();
                }
            }

            return StandardPropertySize;
        }

        protected bool ComparedObjectValueIsTrue(SerializedProperty property)
        {
            var conditionalHelpAttribute = attribute as ConditionalHelpAttribute;
            System.Object objectInstance = property.GetTargetObjectWithProperty();
            FieldInfo field = objectInstance.GetField(conditionalHelpAttribute.TargetConditionName);
            PropertyInfo accessor = objectInstance.GetProperty(conditionalHelpAttribute.TargetConditionName);

            var objectValue = field != null ? field.GetValue(objectInstance) : 
                                              accessor.GetValue(objectInstance);

            if (!objectValue.ToBool(out bool memberValue))
                return false;

            return memberValue;
        }

        public override float GetHelpBoxHeight()
        {
            var helpBoxStyle = (GUI.skin != null) ? GUI.skin.GetStyle("helpbox") : null;

            if (helpBoxStyle == null) 
                return MessageTextPadding;

            var conditionalHelpAttribute = attribute as ConditionalHelpAttribute;
            var content = new GUIContent(conditionalHelpAttribute.HelpText);
            var styleHeight = helpBoxStyle.CalcHeight(content, EditorGUIUtility.currentViewWidth);
            return Mathf.Max(MessageTextPadding, styleHeight);
        }

        protected float GetFullHeight()
        {
            return GetHelpBoxHeight() + InspectorMargin + StandardPropertySize;
        }

        protected abstract bool ShowHelpOnValidComparison();
    }
}