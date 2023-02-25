using System;
using UnityEngine;

namespace PixelSparkStudio.Inspector
{
    /// <summary>
    /// Show a property on the inspector.
    /// </summary>
    
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ShowPropertyAttribute : PropertyAttribute
    {
    }
}