using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Text.RegularExpressions;

namespace YoukaiFox.Inspector
{
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class SceneAttributeDrawer : YoukaiPropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

            if (property.propertyType != SerializedPropertyType.String)
            {
                var message = "ERROR! Not a string field.";
                DrawErrorMessage(position, message);
                return;
            }
            
            var sceneObject = GetSceneObject(property.stringValue);
            var scene = EditorGUI.ObjectField(position, label, sceneObject, typeof(SceneAsset), true);

            if (scene == null) 
            {
                property.stringValue = "";
                return;
            } 
            
            if (!scene.name.Equals(property.stringValue)) 
            {
                if (GetSceneObject(scene.name))
                    property.stringValue = scene.name;
            }
        }
        protected SceneAsset GetSceneObject(string sceneObjectName) {
            if (string.IsNullOrEmpty(sceneObjectName))
                return null;

            foreach (var editorScene in EditorBuildSettings.scenes) 
            {
                if (editorScene.path.IndexOf(sceneObjectName) != -1)
                    return AssetDatabase.LoadAssetAtPath(editorScene.path, typeof(SceneAsset)) as SceneAsset;
            }

            Debug.LogError($"Scene {sceneObjectName} is not added to \"Scenes in Build\" in \"Build Settings\".");
            return null;
        }
    }
}
