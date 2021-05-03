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
    /// Apply this attribute to a method to call it in the inspector via button.
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
        /// The parameters list of the method.
        /// </summary>
        public System.Object[] Arguments = null;

        /// <summary>
        /// Button's color.
        /// </summary>
        public Color Color = Color.clear;

        /// <summary>
        /// Creates a button for a method to call it from the inspector.
        /// </summary>
        /// <param name="buttonMode">Changes when the button should be enabled.</param>
        public ButtonAttribute(EButtonMode buttonMode = EButtonMode.AlwaysEnabled)
        {
            ButtonMode = buttonMode;
            Arguments = null;
        }

        /// <summary>
        /// Creates a button for a method with arguments to call it from the inspector.
        /// </summary>
        /// <param name="color">Button's color.</param>
        /// <param name="buttonMode">Changes when the button should be enabled.</param>
        public ButtonAttribute(EColor color, EButtonMode buttonMode = EButtonMode.AlwaysEnabled)
        {
            Color = Colors.FromEColor(color);
            ButtonMode = buttonMode;
        }

        /// <summary>
        /// Creates a button for a method with arguments to call it from the inspector.
        /// </summary>
        /// <param name="buttonMode">Changes when the button should be enabled.</param>
        /// <param name="arguments">The parameters list of the method.</param>
        public ButtonAttribute(EButtonMode buttonMode = EButtonMode.AlwaysEnabled, params System.Object[] arguments)
        {
            ButtonMode = buttonMode;
            Arguments = arguments;
        }

        /// <summary>
        /// Creates a button for a method to call it from the inspector.
        /// </summary>
        /// <param name="label">Button label shown in the inspector.</param>
        /// <param name="buttonMode">Changes when the button should be enabled.</param>
        public ButtonAttribute(string label, EButtonMode buttonMode = EButtonMode.AlwaysEnabled)
        {
            Label = label;
            ButtonMode = buttonMode;
            Arguments = null;
        }

        /// <summary>
        /// Creates a button for a method with arguments to call it from the inspector.
        /// </summary>
        /// <param name="color">Button's color.</param>
        /// <param name="buttonMode">Changes when the button should be enabled.</param>
        public ButtonAttribute(string label, EColor color, EButtonMode buttonMode = EButtonMode.AlwaysEnabled)
        {
            Label = label;
            Color = Colors.FromEColor(color);
            ButtonMode = buttonMode;
        }

        /// <summary>
        /// Creates a button for a method with arguments to call it from the inspector.
        /// </summary>
        /// <param name="buttonMode">Changes when the button should be enabled.</param>
        /// <param name="arguments">The parameters list of the method.</param>
        public ButtonAttribute(string label, EButtonMode buttonMode = EButtonMode.AlwaysEnabled, params System.Object[] arguments)
        {
            Label = label;
            ButtonMode = buttonMode;
            Arguments = arguments;
        }

        /// <summary>
        /// Creates a button for a method with arguments to call it from the inspector.
        /// </summary>
        /// <param name="color">Button's color.</param>
        /// <param name="buttonMode">Changes when the button should be enabled.</param>
        /// <param name="arguments">The parameters list of the method.</param>
        public ButtonAttribute(string label, EColor color, EButtonMode buttonMode = EButtonMode.AlwaysEnabled, params System.Object[] arguments)
        {
            Label = label;
            Color = Colors.FromEColor(color);
            ButtonMode = buttonMode;
            Arguments = arguments;
        }
    }
}
