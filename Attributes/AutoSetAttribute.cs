using System;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Automatically set the component type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public sealed class AutoSetAttribute : ValidationAttribute
    {
        public bool AlsoSearchInChildren { get; set; }

        public AutoSetAttribute(bool alsoSearchInChildren = false)
        {
            AlsoSearchInChildren = alsoSearchInChildren;
        }
    }
}