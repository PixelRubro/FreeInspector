using System;
using UnityEngine;
using VermillionVanguard.Inspector.Utilities;

namespace VermillionVanguard.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class HighlightAttribute : BaseAttribute
    {
        public Color Color = Color.yellow;
        public string ValidationMethodName = null;
        public string TargetPropertyName = null;
        public object Value = null;

        /// <summary>
        /// Highlight a field.
        /// </summary>
        public HighlightAttribute(EColor color)
        {
            Color = Colors.FromEColor(color);
            ValidationMethodName = null;
            Value = null;
        }

        // /// <summary>
        // /// Highlight a field if <paramref name="targetPropertyName"/>'s value is true.
        // /// </summary>
        // public HighlightAttribute(EColor color, string targetPropertyName)
        // {
        //     Color = Colors.FromEColor(color);
        //     TargetPropertyName = targetPropertyName;
        // }

        // /// <summary>
        // /// Highlight a field if a method with name <paramref name="validationMethodName"/>
        // /// with <paramref name="value"/> as parameter returns true.
        // /// </summary>
        // public HighlightAttribute(EColor color, string validationMethodName, object value)
        // {
        //     Color = Colors.FromEColor(color);
        //     ValidationMethodName = validationMethodName;
        //     Value = value;
        // }
    }
}