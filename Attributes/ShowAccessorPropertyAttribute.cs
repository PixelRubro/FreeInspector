using System;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Show accessor properties on the inspector.
    /// </summary>
    
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ShowAccessorPropertyAttribute : PropertyAttribute
    {
    }
}