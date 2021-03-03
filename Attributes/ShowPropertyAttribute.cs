using System;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Show properties on the inspector.
    /// </summary>
    
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ShowPropertyAttribute : PropertyAttribute
    {
    }
}