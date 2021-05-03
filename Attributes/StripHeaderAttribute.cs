using UnityEngine;
using System;

namespace YoukaiFox.Inspector
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class StripHeaderAttribute : YoukaiAttribute
    {
        public Color BackgroundColor;
        public Color FontColor;
        public string Label;
        public int FontSize;
        public int Height;

        /// <summary>
        /// Full path of the .png or .tiff file starting from the "Assets" folder.
        /// </summary>
        public string IconPath = null;

        public StripHeaderAttribute(EColor color, EColor fontColor, string label,
            int fontSize = 18, int height = 40, string iconPath = null)
        {
            BackgroundColor = Colors.FromEColor(color);
            FontColor = Colors.FromEColor(fontColor);
            Label = label;
            FontSize = fontSize;
            Height = height;
            IconPath = iconPath;
        }
    }
}