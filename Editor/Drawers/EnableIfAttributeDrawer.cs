using UnityEditor;
using UnityEngine;

namespace PixelRouge.Inspector
{
    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    public class EnableIfAttributeDrawer : ConditionalAttributeDrawer
    {
        protected override PropertyDrawing GetPropertyDrawing()
        {
            return PropertyDrawing.Enable;
        }
    }
}