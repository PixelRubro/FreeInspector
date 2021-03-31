using System;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Make the field editable only in play mode.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class PlayModeOnlyAttribute : YoukaiAttribute
    {
    }
}
