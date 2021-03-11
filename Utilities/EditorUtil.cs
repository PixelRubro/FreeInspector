#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector
{
    public static class EditorUtil
    {
        public static int GroupedItemIndent = 15;

        public static float GroupLabelOffset()
        {
            float labelOffset = -5.0f;

            #if UNITY_2019_3_OR_NEWER
                labelOffset = -3.0f;
            #endif

            return labelOffset;
        }

        public static GUIStyle GroupLabelBackgroundStyle()
        {
            var style = new GUIStyle(GUI.skin.box);

            #if UNITY_2019_3_OR_NEWER
                style = new GUIStyle(EditorStyles.helpBox);
            #endif

            return style;
        }

        public static GUIStyle GroupBackgroundStyle()
        {
            var style = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(GroupedItemIndent, GroupedItemIndent, 5, 5)
            };

            #if UNITY_2019_3_OR_NEWER
                style = new GUIStyle(EditorStyles.helpBox)
                {
                    padding = new RectOffset(GroupedItemIndent, GroupedItemIndent, 5, 5)
                };
            #endif

            style.stretchHeight = true;
            return style;
        }

        public static GUIStyle FoldoutStyle()
        {
            var style = EditorStyles.foldout;
            style.margin = new RectOffset(2, 2, 5, 5);
            style.fontStyle = FontStyle.Bold;
            style.stretchHeight = true;
            return style;
        }
    }
}
#endif