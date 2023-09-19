using UnityEngine;
using System;

namespace PixelRouge.Inspector
{
    [AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class InputAttribute : PropertyAttribute
    {
    }
}