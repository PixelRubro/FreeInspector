using System;
using UnityEngine;

namespace SoftBoiledGames.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class TagAttribute : BaseAttribute
    {
    }
}
