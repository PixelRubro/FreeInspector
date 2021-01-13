using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using System.Runtime.CompilerServices;

namespace YoukaiFox.Inspector
{
    public abstract class ConditionalHelpAttributeDrawer : YoukaiPropertyDrawer
    {
        public const int MessageTextPadding = 40;
        public const int InspectorMargin = 4;
        public const int StandardPropertySize = 16;

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

        protected float GetHelpBoxHeight()
        {
            var helpBoxStyle = (GUI.skin != null) ? GUI.skin.GetStyle("helpbox") : null;

            if (helpBoxStyle == null) 
                return MessageTextPadding;

            var conditionalHelpAttribute = attribute as ConditionalHelpAttribute;
            var content = new GUIContent(conditionalHelpAttribute.HelpText);
            return helpBoxStyle.CalcHeight(content, EditorGUIUtility.currentViewWidth);
        }

        protected float GetFullHeight()
        {
            return GetHelpBoxHeight() + InspectorMargin + StandardPropertySize;
        }
    }
}