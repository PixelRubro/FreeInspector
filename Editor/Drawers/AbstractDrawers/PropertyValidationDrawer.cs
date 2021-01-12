using UnityEngine;
using UnityEditor;

namespace YoukaiFox.Inspector
{
    public abstract class PropertyValidationDrawer: YoukaiAttributeDrawer, IWarningMessage
    {
        public abstract string GetWarningMessage();
    }
}