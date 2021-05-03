using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(ShowInPlayModeAttribute))]
    public class ShowInPlayModeAttributeDrawer : YoukaiPropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!Application.isPlaying)
                return 0f;

            return base.GetPropertyHeight(property, label);
        }
    }
}