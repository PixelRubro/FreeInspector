using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(ShowPropertyAttribute))]
    public class ShowPropertyAttributeDrawer : YoukaiPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            throw new System.NotImplementedException();
        }
    }
}