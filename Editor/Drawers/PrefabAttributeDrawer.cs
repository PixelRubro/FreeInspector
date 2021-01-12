using UnityEngine;
using UnityEditor;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(PrefabAttribute))]
    public class PrefabAttributeDrawer : PropertyValidationDrawer 
    {
        public override string GetWarningMessage()
        {
            return "Not a prefab instance!";
        }

        private bool IsPropertyValid(Object value, SerializedProperty property)
        {
            return PrefabUtility.GetPrefabAssetType(value) != PrefabAssetType.NotAPrefab;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label);

            if (EditorGUI.EndChangeCheck())
            {
                var value = property.objectReferenceValue;

                if ((value == null) || (IsPropertyValid(value, property)))
                {
                    return;
                }

                property.objectReferenceValue = null;
            }
        }
    }
}