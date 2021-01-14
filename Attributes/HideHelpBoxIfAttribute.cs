using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Hides help box if provided condition is met.
    /// </summary>
    public sealed class HideHelpBoxIfAttribute : ConditionalHelpAttribute
    {
        /// <summary>
        /// Hides help box if condition is met.
        /// </summary>
        public HideHelpBoxIfAttribute(string targetConditionName, string helpText, MessageType messageType = MessageType.Info)
        : base(targetConditionName, helpText, messageType)
        {
        }
    }
}