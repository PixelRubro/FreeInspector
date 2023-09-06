using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace VermillionVanguard.Inspector
{
    [CustomPropertyDrawer(typeof(IconAttribute))]
    public class IconAttributeDrawer : BasePropertyDrawer
    {
        private Texture2D _iconTexture;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            FindIcon();

            if (_iconTexture)
                label.image = _iconTexture;

            EditorGUI.PropertyField(position, property, label);
        }

        private void FindIcon()
        {
            if (_iconTexture)
                return;

            var iconAttribute = attribute as IconAttribute;

            if (string.IsNullOrEmpty(iconAttribute.IconPath))
            {
                Debug.LogError("ERROR! Path is empty or null.");
                return;
            }

            var path = Path.Combine(Application.dataPath, iconAttribute.IconPath);
            _iconTexture = new Texture2D(1, 1);
            var image = File.ReadAllBytes(path);
            
            if (image == null)
            {
                _iconTexture = null;
                return;
            }
            
            _iconTexture.LoadImage(image);
            _iconTexture.name = "PrefabIcon";
            _iconTexture.Apply();
        }
    }
}
