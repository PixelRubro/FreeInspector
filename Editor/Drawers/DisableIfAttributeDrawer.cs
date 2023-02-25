using UnityEditor;
using UnityEngine;

namespace PixelSparkStudio.Inspector
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