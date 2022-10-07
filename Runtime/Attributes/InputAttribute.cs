using UnityEngine;
using System;

namespace SoftBoiledGames.Inspector
{
    [AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class InputAttribute : PropertyAttribute
    {
    }
}