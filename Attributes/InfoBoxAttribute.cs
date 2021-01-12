using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Show a information box.
    /// </summary>
    public sealed class InfoBoxAttribute : PropertyAttribute
    {
        public string InfoText { get; set; }

        public InfoBoxAttribute(string infoText)
        {
            InfoText = infoText;
        }
    }
}