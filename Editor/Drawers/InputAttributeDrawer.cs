using UnityEngine;
using UnityEditor;

namespace PixelSparkStudio.Inspector
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