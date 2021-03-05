using UnityEngine;
using System;

namespace YoukaiFox.Inspector
{
    [AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class InputAttribute : PropertyAttribute
    {
    }
}