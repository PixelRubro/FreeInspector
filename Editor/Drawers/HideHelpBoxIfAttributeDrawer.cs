using UnityEditor;

namespace PixelRouge.Inspector
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