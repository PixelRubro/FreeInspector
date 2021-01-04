using UnityEngine;

namespace YoukaiFox.Inspector
{
    /// <summary>
    /// Base class for comparison attributes.
    /// </summary>
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

        public ComparisonAttribute(string comparedPropertyName)
        {
            ComparedPropertyName = comparedPropertyName;
            TargetConditionValue = null;
        }

        public ComparisonAttribute(string comparedPropertyName, object targetConditionValue)
        {
            ComparedPropertyName = comparedPropertyName;
            TargetConditionValue = targetConditionValue;
        }

        public ComparisonAttribute(string comparedPropertyName, 
                                   string additionalComparedName, 
                                   ConditionOperator targetCondition)
        {
            ComparedPropertyName = comparedPropertyName;
            AdditionalComparedName = additionalComparedName;
            TargetCondition = targetCondition;
        }

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