using UnityEditor;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(ShowHelpBoxIfAttribute))]
    public class ShowHelpBoxIfAttributeDrawer : ConditionalHelpAttributeDrawer
    {
        protected override bool ShowHelpOnValidComparison()
        {
            return true;
        }
    }
}