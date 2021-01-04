using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Shows property if provided condition is met. Conditional property has to be serialized.
    /// </summary>
    public sealed class ShowIfAttribute : ComparisonAttribute
    {
        /// <summary>
        /// Shows field if the value of 
        /// <paramref name="comparedPropertyName"/> is true.
        /// </summary>
        public ShowIfAttribute(string comparedPropertyName)
            : base(comparedPropertyName)
        {
        }

        /// <summary>
        /// Shows field if the value of <paramref name="comparedPropertyName"/>
        /// is equals to the value of <paramref name="targetConditionValue"/>.
        /// </summary>
        public ShowIfAttribute(string comparedPropertyName, object targetConditionValue)
            : base(comparedPropertyName, targetConditionValue)
        {
        }

        /// <summary>
        /// Shows field if the values in <paramref name="comparedPropertiesNames"/>
        /// under the operator <paramref name="targetCondition"/> returns true.
        /// </summary>
        public ShowIfAttribute(ConditionOperator targetCondition, params string[] comparedPropertiesNames)
            : base(targetCondition, comparedPropertiesNames)
        {
        }

        /// <summary>
        /// Shows field if the values in <paramref name="comparedPropertiesNames"/>
        /// under the operator <paramref name="targetCondition"/> returns a value
        /// equals to the value in <paramref name="targetConditionValue"/>.
        /// </summary>
        public ShowIfAttribute(ConditionOperator targetCondition, object targetConditionValue, params string[] comparedPropertiesNames)
            : base(targetCondition, targetConditionValue, comparedPropertiesNames)
        {
        }
    }
}