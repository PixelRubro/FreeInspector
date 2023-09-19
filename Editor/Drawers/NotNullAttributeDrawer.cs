using UnityEngine;
using UnityEditor;

namespace PixelRouge.Inspector
{
    [CustomPropertyDrawer(typeof(NotNullAttribute))]
    public class NotNullAttributeDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawProperty(position, property, label);
        }
    }
}
