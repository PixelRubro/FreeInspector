using UnityEngine;
using UnityEditor;
using System;

namespace PixelSparkStudio.Inspector
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class SceneAttribute : ValidationAttribute
    {
    }
}
