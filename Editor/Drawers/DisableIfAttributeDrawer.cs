using UnityEditor;
using UnityEngine;

namespace PixelRouge.Inspector
{
    [CustomPropertyDrawer(typeof(DisableIfAttribute))]
    public class DisableIfAttributeDrawer : ConditionalAttributeDrawer
    {
        protected override PropertyDrawing GetPropertyDrawing()
        {
            return PropertyDrawing.Disable;
        }
    }
}