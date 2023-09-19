using System;

namespace PixelRouge.Inspector
{
    /// <summary>
    /// Make the field read-only when the editor is in play mode.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class EnableInPlayModeAttribute : ConditionalAttribute
    {
        public EnableInPlayModeAttribute() : base("true")
        {
        }
    }
}
