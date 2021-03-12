using UnityEngine;
using System;

namespace YoukaiFox.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class ReorderableListAttribute : YoukaiAttribute
    {
    }
}