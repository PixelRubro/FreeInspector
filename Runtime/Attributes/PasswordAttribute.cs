using System;

namespace VermillionVanguard.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class PasswordAttribute : BaseAttribute
    {
    }
}
