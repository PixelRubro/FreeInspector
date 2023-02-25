using UnityEngine;
using System;

namespace PixelSparkStudio.Inspector
{
    [AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class InputAttribute : PropertyAttribute
    {
    }
}