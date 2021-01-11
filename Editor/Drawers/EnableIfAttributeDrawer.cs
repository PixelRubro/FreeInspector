using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    public class EnableIfAttributeDrawer : ConditionalAttributeDrawer<EnableIfAttribute>
    {
        protected override PropertyDrawing GetPropertyDrawing()
        {
            return PropertyDrawing.Enable;
        }
    }
}