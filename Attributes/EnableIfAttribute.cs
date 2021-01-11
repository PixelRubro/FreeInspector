using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Enables property if provided condition is met.
    /// </summary>
    public sealed class EnableIfAttribute : ConditionalAttribute
    {
        /// <summary>
        /// Enables field if <paramref name="comparedPropertyName"/> is true.
        /// </summary>
        public EnableIfAttribute(string comparedPropertyName) : base(comparedPropertyName)
        {
        }

        /// <summary>
        /// Enables field if <paramref name="comparedPropertyName"/>
        /// has a value of <paramref name="targetConditionValue"/>.
        /// </summary>
        public EnableIfAttribute(string comparedPropertyName, object targetConditionValue) : base(comparedPropertyName, targetConditionValue)
        {
        }
    }
}