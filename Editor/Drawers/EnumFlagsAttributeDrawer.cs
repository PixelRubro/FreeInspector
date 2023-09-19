using UnityEngine;
using UnityEditor;

namespace PixelRouge.Inspector
{
    [CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
    public class EnumFlagsAttributeDrawer : BasePropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawEnumFlagsField(position, property, label);
        }
    }
}
