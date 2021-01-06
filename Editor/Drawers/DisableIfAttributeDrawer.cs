using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(DisableIfAttribute))]
    public class DisableIfAttributeDrawer : ConditionalAttributeDrawer<DisableIfAttribute>
    {
        protected override PropertyDrawing GetPropertyDrawing()
        {
            return PropertyDrawing.Disable;
        }
    }
}