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

        protected void DrawDisabledField(Rect position, GUIContent label, string fieldValue)
        {
            EditorGUI.BeginDisabledGroup(true);

            if ((fieldValue.Equals("true")) || (fieldValue.Equals("false")))
                EditorGUI.LabelField(position, label.text, EditorStyles.toggle);
            else
                EditorGUI.LabelField(position, label.text, fieldValue);
                
            EditorGUI.EndDisabledGroup();
        }
    }
}