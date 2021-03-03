using UnityEngine;
using UnityEditor;
using System;

namespace YoukaiFox.Inspector
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class SceneAttribute : ValidationAttribute
    {
    }
}
