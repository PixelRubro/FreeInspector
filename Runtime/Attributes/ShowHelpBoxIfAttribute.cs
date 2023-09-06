namespace VermillionVanguard.Inspector
{
    /// <summary>
    /// Shows a help box if condition is met.
    /// </summary>
    public sealed class ShowHelpBoxIfAttribute : ConditionalHelpAttribute
    {
        /// <summary>
        /// Shows help box if condition is met.
        /// </summary>
        public ShowHelpBoxIfAttribute(string targetConditionName, string helpText, EMessageType messageType = EMessageType.Info)
        : base(targetConditionName, helpText, messageType)
        {
        }
    }
}
