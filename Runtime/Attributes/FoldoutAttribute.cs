using System;

namespace SoftBoiledGames.Inspector
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    /// <summary>
    /// Hide the fields inside a collapsable foldout.
    /// </summary>
    public sealed class FoldoutAttribute : BaseAttribute
    {
        /// <summary>
        /// The name shown on foldout's group label.
        /// </summary>
        public string Name;

        /// <summary>
        /// Hide the fields inside a collapsable foldout.
        /// </summary>
        /// <param name="name"></param>
        public FoldoutAttribute(string name)
        {
            Name = name;
        }
    }
}