using UnityEngine;
using UnityEditor;

namespace YoukaiFox.Inspector
{
    public abstract class PropertyValidationDrawer: YoukaiPropertyDrawer, IWarningMessage
    {
        public abstract string GetWarningMessage();
    }
}