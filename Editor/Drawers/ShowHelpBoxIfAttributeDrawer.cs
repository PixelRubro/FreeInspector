using UnityEditor;

namespace PixelRouge.Inspector
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