using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Shows a help box if condition is met.
    /// </summary>
    public sealed class ShowHelpBoxIfAttribute : ConditionalHelpAttribute
    {
        /// <summary>
        /// Shows help box if condition is met.
        /// </summary>
        public ShowHelpBoxIfAttribute(string targetConditionName, string helpText, MessageType messageType = MessageType.Info)
        : base(targetConditionName, helpText, messageType)
        {
        }
    }
}