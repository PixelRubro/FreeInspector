using System;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    /// <summary>
    /// Group serialized fields with the same in the inspector.
    /// </summary>
    public sealed class GroupAttribute : YoukaiAttribute
    {
        /// <summary>
        /// Group's name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Group serialized fields with the same in the inspector.
        /// </summary>
        /// <param name="name">Group's name.</param>
        public GroupAttribute(string name)
        {
            Name = name;
        }
    }
}