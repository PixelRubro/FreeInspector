using System;

namespace PixelSparkStudio.Inspector
{
    public enum EMessageType
    {
        None, Info, Warning, Error
    }

    /// <summary>
    /// Base class for comparison attributes that show a help box.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class ConditionalHelpAttribute : BaseAttribute
    {
        public string HelpText { get; set; }
        public EMessageType MessageType { get; set; }
        public string TargetConditionName { get; set; }

        /// <summary>
        /// Handles help box display based on the condition.
        /// </summary>
        public ConditionalHelpAttribute(string targetConditionName, string helpText, EMessageType messageType = EMessageType.Info)
        {
            TargetConditionName = targetConditionName;
            HelpText = helpText;
            MessageType = messageType;
        }
    }
}