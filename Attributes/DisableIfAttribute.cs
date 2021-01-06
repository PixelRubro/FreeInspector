using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Disables property if provided condition is met.
    /// </summary>
    public sealed class DisableIfAttribute : ConditionalAttribute
    {
        /// <summary>
        /// Disables field if <paramref name="comparedPropertyName"/> is true.
        /// </summary>
        public DisableIfAttribute(string comparedPropertyName) : base(comparedPropertyName)
        {
        }

        /// <summary>
        /// Disables field if <paramref name="comparedPropertyName"/>
        /// has a value of <paramref name="targetConditionValue"/>.
        /// </summary>
        public DisableIfAttribute(string comparedPropertyName, object targetConditionValue) : base(comparedPropertyName, targetConditionValue)
        {
        }
    }
}