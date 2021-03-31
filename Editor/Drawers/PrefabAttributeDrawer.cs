using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(PrefabAttribute))]
    public class PrefabAttributeDrawer : YoukaiPropertyDrawer 
    {
        private Texture2D _prefabTexture;
        private string _graphicsPath;

        private bool IsPropertyValid(Object value, SerializedProperty property)
        {
            return PrefabUtility.GetPrefabAssetType(value) != PrefabAssetType.NotAPrefab;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            EditorGUI.BeginChangeCheck();
            FindPrefabIcon();

            if (_prefabTexture)
                label.image = _prefabTexture;

            EditorGUI.PropertyField(position, property, label);

            if (EditorGUI.EndChangeCheck())
            {
                var value = property.objectReferenceValue;

                if ((value == null) || (IsPropertyValid(value, property)))
                {
                    return;
                }

                property.objectReferenceValue = null;
            }
        }

        private void FindPrefabIcon()
        {
            if (_prefabTexture)
                return;

            FindGraphicResources();

            if (string.IsNullOrEmpty(_graphicsPath))
                return;

            string path = $"{_graphicsPath}/PrefabIcon.png";

            _prefabTexture = new Texture2D(1, 1);
            _prefabTexture.LoadImage(File.ReadAllBytes(path));
            _prefabTexture.name = "PrefabIcon";
            _prefabTexture.Apply();
        }

        private void FindGraphicResources()
        {
            if (!string.IsNullOrEmpty(_graphicsPath))
                return;

            try
            {
                _graphicsPath = Directory
                    .GetDirectories(Application.dataPath, "*GraphicResources", SearchOption.AllDirectories)
                    .FirstOrDefault();
            }
            catch (System.Exception)
            {
                Debug.Log($"Error when browsing directories under {Application.dataPath}");
                throw;
            }
        }
    }
}