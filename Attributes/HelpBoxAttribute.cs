using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Show a information box.
    /// </summary>
    public sealed class HelpBoxAttribute : PropertyAttribute
    {
        public string HelpText { get; set; }
        public MessageType MessageType { get; set; }

        public HelpBoxAttribute(string helpText, MessageType messageType = MessageType.Info)
        {
            HelpText = helpText;
            MessageType = messageType;
        }
    }
}