using System;

namespace SoftBoiledGames.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class EnumFlagsAttribute : BaseAttribute
    {
    }
}