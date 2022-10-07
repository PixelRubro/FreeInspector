using System;
using UnityEngine;

namespace SoftBoiledGames.Inspector
{
    /// <summary>
    /// Automatically set the component type. Currently only works on serialized fields.
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