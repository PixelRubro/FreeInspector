#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using VermillionVanguard.Inspector.Extensions;

namespace VermillionVanguard.Inspector.Utilities
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

        // public static GUIStyle StripHeaderStyle(StripHeaderAttribute attribute)
        // {
        //     var style = EditorStyles.boldLabel;
        //     style.alignment = TextAnchor.MiddleLeft;
        //     style.padding = new RectOffset(3, 3, 3, 3);
        //     style.richText = true;
        //     style.fontSize = attribute.FontSize;
        //     style.normal.textColor = attribute.FontColor;
        //     style.imagePosition = ImagePosition.ImageLeft;
        //     return style;
        // }

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

        public static bool IsButtonEnabled(UnityEngine.Object target, MethodInfo method)
        {
            var enableIfAttribute = Attribute.GetCustomAttribute(method, typeof(EnableIfAttribute)) as EnableIfAttribute;
            var disableIfAttribute = Attribute.GetCustomAttribute(method, typeof(DisableIfAttribute)) as DisableIfAttribute;
            
            if ((enableIfAttribute == null) && (disableIfAttribute == null))
                return true;

            if (enableIfAttribute != null)
            {
                bool enablingCondition = true;

                if (enableIfAttribute.TargetConditionValue != null)
                {
                    var hasCondition = enableIfAttribute.TargetConditionValue.ToBool(out bool conditionValue);
                    enablingCondition = hasCondition ? conditionValue : true;
                }

                var fieldValue = (bool) target.GetField(enableIfAttribute.PropertyName).GetValue(target);

                if (enablingCondition == fieldValue)
                    return true;
            }

            if (disableIfAttribute != null)
            {
                bool disablingCondition = true;

                if (disableIfAttribute.TargetConditionValue != null)
                {
                    var hasCondition = disableIfAttribute.TargetConditionValue.ToBool(out bool conditionValue);
                    disablingCondition = hasCondition ? conditionValue : true;
                }

                var fieldValue = (bool) target.GetField(disableIfAttribute.PropertyName).GetValue(target);

                if (disablingCondition != fieldValue)
                    return true;
            }

            return false;
        }

        public static bool IsButtonVisible(UnityEngine.Object target, MethodInfo method)
        {
            var showIfAttribute = Attribute.GetCustomAttribute(method, typeof(ShowIfAttribute)) as ShowIfAttribute;
            var hideIfAttribute = Attribute.GetCustomAttribute(method, typeof(HideIfAttribute)) as HideIfAttribute;

            if ((showIfAttribute == null) && (hideIfAttribute == null))
                return true;

            if (showIfAttribute != null)
            {
                bool showingCondition = true;

                if (showIfAttribute.TargetConditionValue != null)
                {
                    var hasCondition = showIfAttribute.TargetConditionValue.ToBool(out bool conditionValue);
                    showingCondition = hasCondition ? conditionValue : true;
                }

                var fieldValue = (bool) target.GetField(showIfAttribute.PropertyName).GetValue(target);

                if (showingCondition == fieldValue)
                    return true;
            }

            if (hideIfAttribute != null)
            {
                bool hidingCondition = true;

                if (hideIfAttribute.TargetConditionValue != null)
                {
                    var hasCondition = hideIfAttribute.TargetConditionValue.ToBool(out bool conditionValue);
                    hidingCondition = hasCondition ? conditionValue : true;
                }

                var fieldValue = (bool) target.GetField(hideIfAttribute.PropertyName).GetValue(target);

                if (hidingCondition != fieldValue)
                    return true;
            }

            return false;
        }

        public static List<bool> GetConditionValues(object target, string[] conditions)
        {
            List<bool> conditionValues = new List<bool>();
            foreach (var condition in conditions)
            {
                FieldInfo conditionField = target.GetField(condition);
                if (conditionField != null &&
                    conditionField.FieldType == typeof(bool))
                {
                    conditionValues.Add((bool)conditionField.GetValue(target));
                }

                PropertyInfo conditionProperty = target.GetProperty(condition);
                if (conditionProperty != null &&
                    conditionProperty.PropertyType == typeof(bool))
                {
                    conditionValues.Add((bool)conditionProperty.GetValue(target));
                }

                MethodInfo conditionMethod = target.GetMethod(condition);
                if (conditionMethod != null &&
                    conditionMethod.ReturnType == typeof(bool) &&
                    conditionMethod.GetParameters().Length == 0)
                {
                    conditionValues.Add((bool)conditionMethod.Invoke(target, null));
                }
            }

            return conditionValues;
        }

        public static bool GetConditionsFlag(List<bool> conditionValues)
        {
            bool flag = false;

            foreach (var value in conditionValues)
            {
                flag = flag && value;
            }

            return flag;
        }
    }
}
#endif