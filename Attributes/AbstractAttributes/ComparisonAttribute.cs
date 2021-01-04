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
        public string ComparedPropertyName { get; private set; }
        public string AdditionalComparedName { get; private set; }
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
            ComparedPropertyName = comparedPropertyName;
            TargetConditionValue = null;
        }

        /// <summary>
        /// Comparison is true if <paramref name="comparedPropertyName"/>
        /// has a value of <paramref name="targetConditionValue"/>.
        /// </summary>
        public ComparisonAttribute(string comparedPropertyName, object targetConditionValue)
        {
            ComparedPropertyName = comparedPropertyName;
            TargetConditionValue = targetConditionValue;
        }

        /// <summary>
        /// Consider the boolean values of <paramref name="comparedPropertyName"/>
        /// and <paramref name="additionalComparedName"/> under the operator
        /// <paramref name="targetCondition"/>.
        /// </summary>
        public ComparisonAttribute(string comparedPropertyName, 
                                   string additionalComparedName, 
                                   ConditionOperator targetCondition)
        {
            ComparedPropertyName = comparedPropertyName;
            AdditionalComparedName = additionalComparedName;
            TargetCondition = targetCondition;
        }

        /// <summary>
        /// Consider the boolean values of <paramref name="comparedPropertyName"/>
        /// and <paramref name="additionalComparedName"/> under the operator
        /// <paramref name="targetCondition"/> and is true if it's equals to
        /// <paramref name="targetConditionValue"/>.
        /// </summary>
        public ComparisonAttribute(string comparedPropertyName, 
                                   string additionalComparedName, 
                                   ConditionOperator targetCondition, 
                                   object targetConditionValue)
        {
            ComparedPropertyName = comparedPropertyName;
            AdditionalComparedName = additionalComparedName;
            TargetCondition = targetCondition;
            TargetConditionValue = targetConditionValue;
        }
    }
}