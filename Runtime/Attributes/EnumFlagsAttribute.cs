using System;

namespace PixelSparkStudio.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class EnumFlagsAttribute : BaseAttribute
    {
    }
}