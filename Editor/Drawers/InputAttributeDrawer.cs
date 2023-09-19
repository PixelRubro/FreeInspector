using UnityEngine;
using UnityEditor;

namespace PixelRouge.Inspector
{
    [CustomPropertyDrawer(typeof(InputAttribute))]
    public class InputAttributeDrawer : BasePropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            DrawInputField(position, property, label);
        }
    }
}