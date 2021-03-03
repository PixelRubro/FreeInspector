using UnityEngine;
using System;
using UnityEditor;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Base class for comparison attributes that show a help box.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class ConditionalHelpAttribute : YoukaiAttribute
    {
        public string HelpText { get; set; }
        public MessageType MessageType { get; set; }
        public string TargetConditionName { get; set; }

        /// <summary>
        /// Handles help box display based on the condition.
        /// </summary>
        public ConditionalHelpAttribute(string targetConditionName, string helpText, MessageType messageType = MessageType.Info)
        {
            TargetConditionName = targetConditionName;
            HelpText = helpText;
            MessageType = messageType;
        }
    }
}