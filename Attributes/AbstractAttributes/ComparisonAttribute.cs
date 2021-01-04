using UnityEngine;
using System;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Base class for comparison attributes.
    /// </summary>
    // [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public abstract class ComparisonAttribute : PropertyAttribute
    {
        public string[] PropertiesNames { get; private set; }
        public ConditionOperator TargetCondition { get; private set; }
        public object TargetConditionValue { get; private set; }

        public enum ConditionOperator
        {
            AND,
            OR
        }

        /// <summary>
        /// Comparison is true if <paramref name="comparedPropertyName"/>
        /// is true.
        /// </summary>
        public ComparisonAttribute(string comparedPropertyName)
        {
            PropertiesNames = new string[1] { comparedPropertyName };
            TargetConditionValue = null;
        }

        /// <summary>
        /// Comparison is true if <paramref name="comparedPropertyName"/>
        /// has a value of <paramref name="targetConditionValue"/>.
        /// </summary>
        public ComparisonAttribute(string comparedPropertyName, object targetConditionValue)
        {
            PropertiesNames = new string[1] { comparedPropertyName };
            TargetConditionValue = targetConditionValue;
        }

        /// <summary>
        /// Consider the boolean values of <paramref name="comparedPropertiesNames"/>
        /// under the operator <paramref name="targetCondition"/>.
        /// </summary>
        public ComparisonAttribute(ConditionOperator targetCondition, params string[] comparedPropertiesNames)
        {
            TargetCondition = targetCondition;
            PropertiesNames = comparedPropertiesNames;
        }

        /// <summary>
        /// Consider the boolean values of <paramref name="comparedPropertyName"/>
        /// and <paramref name="additionalComparedName"/> under the operator
        /// <paramref name="targetCondition"/> and is true if it's equals to
        /// <paramref name="targetConditionValue"/>.
        /// </summary>
        public ComparisonAttribute(ConditionOperator targetCondition, object targetConditionValue, params string[] comparedPropertiesNames)
        {
            TargetCondition = targetCondition;
            TargetConditionValue = targetConditionValue;
            PropertiesNames = comparedPropertiesNames;
        }
    }
}