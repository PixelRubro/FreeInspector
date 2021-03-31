using UnityEngine;
using UnityEditor;
using YoukaiFox.Inspector.Extensions;

namespace YoukaiFox.Inspector
{
    public abstract class YoukaiPropertyDrawer: PropertyDrawer 
    {
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
            if (property.GetAttribute<ReadOnlyAttribute>() != null)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.PropertyField(position, property, label, property.isExpanded);
                EditorGUI.EndDisabledGroup();
                return;
            }

            EditorGUI.PropertyField(position, property);
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
    }
}