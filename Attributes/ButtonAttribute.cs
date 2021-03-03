using System.Collections;
using System;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    public enum EButtonMode
    {
        AlwaysEnabled, EditorOnly, PlayModeOnly
    }

    [System.AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    /// <summary>
    /// Apply this attribute to a method with no arguments for 
    /// calling it in the inspector via a button.
    /// </summary>
    public sealed class ButtonAttribute : PropertyAttribute
    {
        /// <summary>
        /// Button label shown in the inspector.
        /// </summary>
        public readonly string Label;

        /// <summary>
        /// Changes when the button should be enabled.
        /// </summary>
        public EButtonMode ButtonMode;

        /// <summary>
        /// Apply this attribute to a method with no arguments for 
        /// calling it in the inspector via a button.
        /// </summary>
        /// <param name="buttonMode">Changes when the button should be enabled.</param>
        public ButtonAttribute(EButtonMode buttonMode = EButtonMode.AlwaysEnabled)
        {
            ButtonMode = buttonMode;
        }

        /// <summary>
        /// Apply this attribute to a method with no arguments for 
        /// calling it in the inspector via a button.
        /// </summary>
        /// <param name="label">Button label shown in the inspector.</param>
        /// <param name="buttonMode">Changes when the button should be enabled.</param>
        public ButtonAttribute(string label, EButtonMode buttonMode = EButtonMode.AlwaysEnabled)
        {
            Label = label;
            ButtonMode = buttonMode;
        }
    }
}
