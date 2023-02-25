using UnityEditor;

namespace PixelSparkStudio.Inspector
{
    [CustomPropertyDrawer(typeof(HideHelpBoxIfAttribute))]
    public class HideHelpBoxIfAttributeDrawer : ConditionalHelpAttributeDrawer
    {
        protected override bool ShowHelpOnValidComparison()
        {
            return false;
        }
    }
}