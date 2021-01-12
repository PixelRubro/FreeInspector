using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(InfoBoxAttribute))]
    public class InfoBoxAttributeDrawer : YoukaiAttributeDrawer
    {
        private const int MessageTextPadding = 40;

        private const int InspectorMargin = 4;

        private float _backupHeight = 0;

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            var infoBoxAttribute = attribute as InfoBoxAttribute;
            _backupHeight = base.GetPropertyHeight(prop, label);
            float minHeight = MessageTextPadding;

            // Calculate the height of the HelpBox using the GUIStyle on the current skin and the inspector
            // window's currentViewWidth.
            var content = new GUIContent(infoBoxAttribute.InfoText);
            var style = GUI.skin.GetStyle("helpbox");
            var height = style.CalcHeight(content, EditorGUIUtility.currentViewWidth);
            height += InspectorMargin;

            return height > minHeight ? height + _backupHeight : minHeight + _backupHeight;
        }


        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            var infoBoxAttribute = attribute as InfoBoxAttribute;
            EditorGUI.BeginProperty(position, label, prop);
            var backupPosition = position;
            backupPosition.height -= _backupHeight + InspectorMargin;

            EditorGUI.HelpBox(backupPosition, infoBoxAttribute.InfoText, MessageType.Info);

            position.y += backupPosition.height + InspectorMargin;
            position.height = _backupHeight;

            EditorGUI.PropertyField(position, prop, label);
            EditorGUI.EndProperty();
        }
    }
}