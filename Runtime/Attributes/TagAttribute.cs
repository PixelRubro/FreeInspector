using System;
using UnityEngine;

namespace PixelRouge.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class TagAttribute : BaseAttribute
    {
    }
}
