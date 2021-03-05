using UnityEngine;
using UnityEditor;

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
        
        // public void DrawDefaultPropertyAndHelpBox(Rect rect, SerializedProperty property, string message, MessageType messageType)
		// {
		// 	float indentLength = NaughtyEditorGUI.GetIndentLength(rect);
		// 	Rect helpBoxRect = new Rect(
		// 		rect.x + indentLength,
		// 		rect.y,
		// 		rect.width - indentLength,
		// 		GetHelpBoxHeight());

		// 	NaughtyEditorGUI.HelpBox(helpBoxRect, message, MessageType.Warning, context: property.serializedObject.targetObject);

		// 	Rect propertyRect = new Rect(
		// 		rect.x,
		// 		rect.y + GetHelpBoxHeight(),
		// 		rect.width,
		// 		GetPropertyHeight(property));

		// 	EditorGUI.PropertyField(propertyRect, property, true);
		// }
    }
}