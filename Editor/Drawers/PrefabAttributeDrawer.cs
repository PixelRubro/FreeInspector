using UnityEngine;
using UnityEditor;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(PrefabAttribute))]
    public class PrefabAttributeDrawer: PropertyValidationDrawer 
    {
        protected override bool IsObjectValid(Object value, SerializedProperty property)
        {
            return PrefabUtility.GetPrefabAssetType(value) != PrefabAssetType.NotAPrefab;
        }
    }
}