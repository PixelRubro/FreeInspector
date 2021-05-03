#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector.Utilities
{
    public static class EditorUtil
    {
        public static int FoldoutIndent = 1;
        public static int GroupedItemIndent = 15;

        public static float GroupLabelOffset()
        {
            float labelOffset = -5.0f;

            #if UNITY_2019_3_OR_NEWER
                labelOffset = -3.0f;
            #endif

            return labelOffset;
        }

        public static GUIStyle GroupLabelStyle()
        {
            var style = EditorStyles.boldLabel;
            style.alignment = TextAnchor.MiddleLeft;
            style.fontSize = 12;
            style.normal.textColor = Color.white;
            return style;
        }

        public static GUIStyle GroupLabelBackgroundStyle()
        {
            var style = new GUIStyle(GUI.skin.box);

            #if UNITY_2019_3_OR_NEWER
                style = new GUIStyle(EditorStyles.helpBox);
            #endif

            style.normal.textColor = Color.white;
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

            // style = new GUIStyle(GUI.skin.box);
            style.alignment = TextAnchor.MiddleLeft;
            style.fontSize = 12;
            style.stretchHeight = true;
            return style;
        }

        public static GUIStyle FoldoutStyle()
        {
            var style = EditorStyles.foldout;
            style.margin = new RectOffset(2, 2, 5, 5);
            style.fontStyle = FontStyle.Bold;
            style.stretchHeight = true;
            style.alignment = TextAnchor.MiddleLeft;
            style.fontSize = 12;
            style.normal.textColor = Color.white;
            return style;
        }

        public static GUIStyle UngroupedFieldsStyle()
        {
            var style = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(GroupedItemIndent, GroupedItemIndent, 5, 5)
            };

            style.alignment = TextAnchor.MiddleLeft;
            style.stretchHeight = true;
            style.alignment = TextAnchor.MiddleLeft;
            style.fontSize = 12;
            return style;
        }

        public static GUIStyle HorizontalLineStyle()
        {
            var lineStyle = new GUIStyle();
            lineStyle.normal.background = EditorGUIUtility.whiteTexture;
            lineStyle.margin = new RectOffset(2, 2, 5, 5);
            lineStyle.fixedHeight = 1;
            return lineStyle;
        }

        public static GUIStyle StripHeaderStyle(StripHeaderAttribute attribute)
        {
            var style = EditorStyles.boldLabel;
            style.alignment = TextAnchor.MiddleLeft;
            style.padding = new RectOffset(3, 3, 3, 3);
            style.richText = true;
            style.fontSize = attribute.FontSize;
            style.normal.textColor = attribute.FontColor;
            style.imagePosition = ImagePosition.ImageLeft;
            return style;
        }

		public static Type GetListElementType(Type listType)
		{
			if (listType.IsGenericType)
			{
				return listType.GetGenericArguments()[0];
			}
			else
			{
				return listType.GetElementType();
			}
		}

        public static float GetRectIndentLength(Rect rect)
        {
            Rect indentRect = EditorGUI.IndentedRect(rect);
            float indentLength = indentRect.x - rect.x;
            return indentLength;
        }

        public static Texture2D FindIcon(string iconPath)
        {
            var path = Path.Combine(Application.dataPath, iconPath);
            var image = File.ReadAllBytes(path);

            if (image == null)
                return null;

            var iconTexture = new Texture2D(1, 1);
            iconTexture.LoadImage(image);
            iconTexture.name = "HeaderIcon";
            iconTexture.Apply();
            return iconTexture;
        }
    }
}
#endif