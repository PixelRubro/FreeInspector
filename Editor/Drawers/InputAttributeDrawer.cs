using UnityEngine;
using UnityEditor;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(InputAttribute))]
    public class InputAttributeDrawer : YoukaiPropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            DrawInputField(position, property, label);
        }
    }
}