using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Prevent the field from being edited in inspector.
    /// </summary>
    public sealed class ReadOnlyAttribute : PropertyAttribute
    {
    }
}