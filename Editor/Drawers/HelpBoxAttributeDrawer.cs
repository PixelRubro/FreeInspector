using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(HelpBoxAttribute))]
    public class HelpBoxAttributeDrawer : YoukaiDecoratorDrawer
    {
        private const int MessageTextPadding = 40;
        private const int InspectorMargin = 4;

        public override float GetHeight() 
        {
            var helpBoxStyle = (GUI.skin != null) ? GUI.skin.GetStyle("helpbox") : null;

            if (helpBoxStyle == null) 
                return base.GetHeight();

            var helpBoxAttribute = attribute as HelpBoxAttribute;
            var content = new GUIContent(helpBoxAttribute.HelpText);
            var styleMargin = helpBoxStyle.CalcHeight(content, EditorGUIUtility.currentViewWidth) + InspectorMargin;
            return Mathf.Max(MessageTextPadding, styleMargin);
        }

        public override void OnGUI(Rect position) 
        {
            var helpBoxAttribute = attribute as HelpBoxAttribute;
            EditorGUI.HelpBox(position, helpBoxAttribute.HelpText, ConvertMessageType(helpBoxAttribute.MessageType));
        }
    }
}