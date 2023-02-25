using UnityEditor;
using UnityEngine;

namespace PixelSparkStudio.Inspector
{
    /// <summary>
    /// Show a information box.
    /// </summary>
    public sealed class HelpBoxAttribute : PropertyAttribute
    {
        public string HelpText { get; set; }
        public EMessageType MessageType { get; set; }

        public HelpBoxAttribute(string helpText, EMessageType messageType = EMessageType.Info)
        {
            HelpText = helpText;
            MessageType = messageType;
        }
    }
}