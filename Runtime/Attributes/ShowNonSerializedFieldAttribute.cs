using System;
using UnityEngine;

namespace VermillionVanguard.Inspector
{
    /// <summary>
    /// Show non-serialized fields on inspector.
    /// </summary>
    
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ShowNonSerializedFieldAttribute : PropertyAttribute
    {
    }
}