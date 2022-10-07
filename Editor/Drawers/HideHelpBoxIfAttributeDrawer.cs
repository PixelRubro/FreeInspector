using UnityEditor;

namespace SoftBoiledGames.Inspector
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