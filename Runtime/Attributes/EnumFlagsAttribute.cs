using System;

namespace PixelRouge.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class EnumFlagsAttribute : BaseAttribute
    {
    }
}