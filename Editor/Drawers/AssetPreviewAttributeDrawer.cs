using UnityEngine;
using UnityEditor;
using SoftBoiledGames.Inspector.Utilities;

namespace SoftBoiledGames.Inspector
{
    [CustomPropertyDrawer(typeof(AssetPreviewAttribute))]
    public class AssetPreviewAttributeDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                var errorMessage = "ERROR! The AssetPreview attribute can't be used with this field type.";
                DrawErrorMessage(position, errorMessage);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            Rect propertyRect = new Rect()
            {
                x = position.x,
                y = position.y,
                width = position.width,
                height = EditorGUIUtility.singleLineHeight
            };

            EditorGUI.PropertyField(propertyRect, property, label);
            Texture2D previewTexture = GetAssetPreview(property);

            if (previewTexture != null)
            {
                Rect previewRect = new Rect()
                {
                    x = position.x + EditorUtil.GetRectIndentLength(position),
                    y = position.y + EditorGUIUtility.singleLineHeight,
                    width = position.width,
                    height = GetAssetPreviewSize(property).y
                };

                GUI.Label(previewRect, previewTexture);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
                return base.GetPropertyHeight(property, label);

            Texture2D previewTexture = GetAssetPreview(property);

            if (previewTexture == null)
                return base.GetPropertyHeight(property, label);

            return base.GetPropertyHeight(property, label) + GetAssetPreviewSize(property).y;
        }

        private Texture2D GetAssetPreview(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (property.objectReferenceValue != null)
                {
                    Texture2D previewTexture = AssetPreview.GetAssetPreview(property.objectReferenceValue);
                    return previewTexture;
                }
            }

            return null;
        }

        private Vector2 GetAssetPreviewSize(SerializedProperty property)
        {
            Texture2D previewTexture = GetAssetPreview(property);

            if (previewTexture == null)
                return Vector2.zero;
            
            var previewAttribute = attribute as AssetPreviewAttribute;

            int width = Mathf.Clamp(previewAttribute.Width, 0, previewTexture.width);
            int height = Mathf.Clamp(previewAttribute.Height, 0, previewTexture.height);

            return new Vector2(width, height);
        }
    }
}
