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

        protected override bool IsObjectValid(Object value, SerializedProperty property)
        {
            return PrefabUtility.GetPrefabAssetType(value) != PrefabAssetType.NotAPrefab;
        }
    }
}