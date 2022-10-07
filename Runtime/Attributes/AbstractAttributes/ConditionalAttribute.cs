using System;

namespace SoftBoiledGames.Inspector
{
    /// <summary>
    /// Base class for comparison attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ConditionalAttribute : BaseAttribute
    {
        // public string[] PropertiesNames { get; private set; }
        public string PropertyName = null;
        // public ConditionOperator TargetCondition { get; private set; }
        public System.Object TargetConditionValue = null;

        // public enum ConditionOperator
        // {
        //     NONE,
        //     AND,
        //     OR
        // }

        /// <summary>
        /// Comparison is true if <paramref name="comparedPropertyName"/>
        /// is true.
        /// </summary>
        public ConditionalAttribute(string comparedPropertyName)
        {
            PropertyName = comparedPropertyName;
            TargetConditionValue = null;
        }

        /// <summary>
        /// Comparison is true if <paramref name="comparedPropertyName"/>
        /// has a value of <paramref name="targetConditionValue"/>.
        /// </summary>
        public ConditionalAttribute(string comparedPropertyName, System.Object targetConditionValue)
        {
            PropertyName = comparedPropertyName;
            TargetConditionValue = targetConditionValue;
        }

        // /// <summary>
        // /// Comparison is true if <paramref name="comparedPropertyName"/>
        // /// is true.
        // /// </summary>
        // public ConditionalAttribute(string comparedPropertyName)
        // {
        //     PropertiesNames = new string[1] { comparedPropertyName };
        //     TargetCondition = ConditionOperator.NONE;
        //     TargetConditionValue = null;
        // }

        // /// <summary>
        // /// Comparison is true if <paramref name="comparedPropertyName"/>
        // /// has a value of <paramref name="targetConditionValue"/>.
        // /// </summary>
        // public ConditionalAttribute(string comparedPropertyName, object targetConditionValue)
        // {
        //     PropertiesNames = new string[1] { comparedPropertyName };
        //     TargetCondition = ConditionOperator.NONE;
        //     TargetConditionValue = targetConditionValue;
        // }

        // /// <summary>
        // /// Consider the boolean values of <paramref name="comparedPropertiesNames"/>
        // /// under the operator <paramref name="targetCondition"/>.
        // /// </summary>
        // public ConditionalAttribute(ConditionOperator targetCondition, params string[] comparedPropertiesNames)
        // {
        //     TargetCondition = targetCondition;
        //     PropertiesNames = comparedPropertiesNames;
        //     TargetConditionValue = null;
        // }

        // /// <summary>
        // /// Consider the boolean values of <paramref name="comparedPropertyName"/>
        // /// and <paramref name="additionalComparedName"/> under the operator
        // /// <paramref name="targetCondition"/> and is true if it's equals to
        // /// <paramref name="targetConditionValue"/>.
        // /// </summary>
        // public ConditionalAttribute(ConditionOperator targetCondition, object targetConditionValue, params string[] comparedPropertiesNames)
        // {
        //     TargetCondition = targetCondition;
        //     TargetConditionValue = targetConditionValue;
        //     PropertiesNames = comparedPropertiesNames;
        // }
    }
}