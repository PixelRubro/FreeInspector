using UnityEngine;
using UnityEditor;

namespace YoukaiFox.Inspector
{
    public abstract class PropertyValidationDrawer: PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label);

            if (EditorGUI.EndChangeCheck())
            {
                var value = property.objectReferenceValue;

                if (value == null || IsObjectValid(value, property))
                    return;

                property.objectReferenceValue = null;
            }
        }

        protected abstract bool IsObjectValid(Object value, SerializedProperty property);
    }
}