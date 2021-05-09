using UnityEngine;
using UnityEditor;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(NotNullAttribute))]
    public class NotNullAttributeDrawer : YoukaiPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawProperty(position, property, label);
        }
    }
}
