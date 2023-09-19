using UnityEngine;
using UnityEditor;
using System;

namespace PixelRouge.Inspector
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class SceneAttribute : ValidationAttribute
    {
    }
}
