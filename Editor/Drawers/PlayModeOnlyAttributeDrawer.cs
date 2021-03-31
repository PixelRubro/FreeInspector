using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(PlayModeOnlyAttribute))]
    public class PlayModeOnlyAttributeDrawer : YoukaiPropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!Application.isPlaying) 
                GUI.enabled = false;
                
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
