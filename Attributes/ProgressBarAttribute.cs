using System;

namespace YoukaiFox.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    /// <summary>
    /// Draw a progress bar for a serialized float field in the inspector which
    /// goes from 0 to 1.
    /// </summary>
    public sealed class ProgressBarAttribute : YoukaiAttribute
    {
        /// <summary>
        /// Display name.
        /// </summary>
        public string Label;

        /// <summary>
        /// Hide bar when field value is zero.
        /// </summary>
        public bool HideWhenZero;

        /// <summary>
        /// Draw a progress bar for a serialized float field in the inspector.
        /// </summary>
        /// <param name="label">Display name.</param>
        /// <param name="hideWhenZero">Hide bar when field value is zero.</param>
        public ProgressBarAttribute(string label, bool hideWhenZero = false)
        {
            Label = label;
            HideWhenZero = hideWhenZero;
        }
    }
}