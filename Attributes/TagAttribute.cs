using System;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class TagAttribute : YoukaiAttribute
    {
    }
}
