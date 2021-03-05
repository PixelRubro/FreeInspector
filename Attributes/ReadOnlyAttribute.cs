using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Prevent a serialized field from being edited in inspector.
    /// </summary>
    public sealed class ReadOnlyAttribute : PropertyAttribute
    {
    }
}