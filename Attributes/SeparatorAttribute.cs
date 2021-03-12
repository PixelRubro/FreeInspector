using UnityEngine;
using System;

namespace YoukaiFox.Inspector
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class SeparatorAttribute : YoukaiAttribute
    {
        public Color Color = Color.gray;
        public int Thickness = 1;

        public SeparatorAttribute(int thickness = 1)
        {
            Thickness = thickness;
        }

        public SeparatorAttribute(EColor color, int thickness = 1)
        {
            Color = Colors.FromEColor(color);
            Thickness = thickness;
        }
    }
}