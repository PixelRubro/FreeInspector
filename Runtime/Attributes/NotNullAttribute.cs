using System;
using UnityEngine;

namespace VermillionVanguard.Inspector
{
    /// <summary>
    /// Display an warning in the inspector if the field's value is null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public sealed class NotNullAttribute : PropertyAttribute
    {
    }
}