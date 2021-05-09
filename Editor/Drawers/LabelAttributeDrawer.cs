using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeDrawer : YoukaiPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawProperty(position, property, label);
        }
    }
}