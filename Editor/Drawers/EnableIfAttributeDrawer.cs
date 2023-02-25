using UnityEditor;
using UnityEngine;

namespace PixelSparkStudio.Inspector
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