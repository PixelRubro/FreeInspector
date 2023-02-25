using System;

namespace PixelSparkStudio.Inspector
{
    /// <summary>
    /// Make the field read-only when the editor is in play mode.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class DisableInPlayModeAttribute : ConditionalAttribute
    {
        public DisableInPlayModeAttribute() : base("true")
        {
        }
    }
}
