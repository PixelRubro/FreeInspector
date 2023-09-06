using System;
using UnityEngine;

namespace VermillionVanguard.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class TagAttribute : BaseAttribute
    {
    }
}
