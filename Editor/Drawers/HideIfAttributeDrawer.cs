using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIfAttributeDrawer : ConditionalAttributeDrawer<HideIfAttribute>
    {
        protected override PropertyDrawing GetPropertyDrawing()
        {
            return PropertyDrawing.Hide;
        }
    }
}