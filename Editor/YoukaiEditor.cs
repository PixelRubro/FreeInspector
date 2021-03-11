using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace YoukaiFox.Inspector
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class YoukaiEditor : Editor 
    {
        #region Fields

        #region Static fields
        #endregion

        #region Attributes references

        private IEnumerable<SerializedProperty> _groupedFields;
        private IEnumerable<SerializedProperty> _foldoutGroupFields;
        private IEnumerable<FieldInfo> _ungroupedFields;
        private IEnumerable<FieldInfo> _nonSerializedFields;
        private IEnumerable<FieldInfo> _serializedFields;
        private IEnumerable<PropertyInfo> _nativeProperties;
        private IEnumerable<MethodInfo> _methods;
        private IEnumerable<MethodInfo> _methodsNoArguments;
        private IEnumerable<MethodInfo> _methodsWithArguments;

        #endregion

        private List<SerializedProperty> _serializedProperties;
        private Dictionary<string, SavedBool> _foldoutStates = new Dictionary<string, SavedBool>();

        #endregion
        
        private void OnEnable() 
        {
            _serializedProperties = new List<SerializedProperty>();
            _serializedProperties = FindSerializedProperties(_serializedProperties);
            FindAttributes();
        }

        public override void OnInspectorGUI() 
        {
            serializedObject.Update();
            DrawDefaultInspector();
            _serializedProperties = FindSerializedProperties(_serializedProperties);
            DrawGroups();
            DrawFoldoutGroups();
            DrawButtons();
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(serializedObject.targetObject);
            Repaint();
        }

        private void DrawButtons()
        {
            if (!_methods.Any())
                return;

            serializedObject.Update();

            foreach (var method in _methods)
            {
                var buttonAttribute = Attribute.GetCustomAttribute(method, typeof(ButtonAttribute)) as ButtonAttribute;

                if (buttonAttribute == null)
                    continue;

                var previousGuiStatus = GUI.enabled;

                if (buttonAttribute.ButtonMode == EButtonMode.AlwaysEnabled)
                    GUI.enabled = true;
                else if (buttonAttribute.ButtonMode == EButtonMode.EditorOnly)
                    GUI.enabled = !EditorApplication.isPlaying;
                else
                    GUI.enabled = EditorApplication.isPlaying;

                var buttonName = String.IsNullOrEmpty(buttonAttribute.Label) ?
                    ObjectNames.NicifyVariableName(method.Name) :
                    buttonAttribute.Label;

                if (!GUILayout.Button(buttonName))
                    continue;
                
                var defaultParameters = method.GetParameters().Select(p => p.DefaultValue).ToArray();
                method.Invoke(serializedObject.targetObject, defaultParameters);
                GUI.enabled = previousGuiStatus;
            }
        }

        private void DrawGroups()
        {
            serializedObject.Update();
            var visibleFields = _groupedFields.Where(f => f.IsVisible());
            var groupedFields = visibleFields.GroupBy(f => f.GetAttribute<GroupAttribute>().Name);

            foreach (var group in groupedFields)
            {
                BeginGroup(group.Key);

                foreach (var prop in group)
                {
                    using (var scope = new EditorGUILayout.VerticalScope(EditorUtil.GroupBackgroundStyle()))
                    {
                        EditorGUILayout.PropertyField(prop);
                    };
                }

                EndGroup();
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawFoldoutGroups()
        {
            serializedObject.Update();
            var visibleFields = _foldoutGroupFields.Where(f => f.IsVisible());
            var foldoutFields = visibleFields.GroupBy(f => f.GetAttribute<FoldoutAttribute>().Name);

            foreach (var group in foldoutFields)
            {
                var key = group.Key;

                if (!_foldoutStates.ContainsKey(key))
                {
                    var savedBool = new SavedBool($"{target.GetInstanceID()}.{key}", false);
                    _foldoutStates.Add(key, savedBool);
                }
                
                _foldoutStates[key].Value = EditorGUILayout.Foldout(_foldoutStates[key].Value, key, EditorUtil.FoldoutStyle());

                if (_foldoutStates[key].Value)
                {
                    foreach (var prop in group)
                    {
                        EditorGUILayout.PropertyField(prop);
                    }
                }
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawUngroupedFields()
        {
            serializedObject.Update();
            var visibleFields = _groupedFields.Where(f => f.IsVisible());
        }

        private void FindAttributes()
        {
            _serializedFields = target.
                GetAllFields(f => f.GetCustomAttributes(typeof(SerializeField), true).Length > 0);

            // _properties = target.
            //     GetAllProperties(p => p.GetCustomAttributes(typeof(ShowPropertyAttribute), true).Length > 0);

            _methods = target.
                GetAllMethods(m => m.GetCustomAttributes(typeof(ButtonAttribute), true).Length > 0);

            _methodsNoArguments = _methods.Where(m => m.GetParameters().Length == 0);
            _methodsWithArguments = _methods.Where(m => m.GetParameters().Length > 0);

            _groupedFields = _serializedProperties
                .Where(p => p.GetAttribute<GroupAttribute>() != null);
                // .GroupBy(f => f.Name);

            _foldoutGroupFields = _serializedProperties
                .Where(f => f.GetAttribute<FoldoutAttribute>() != null);

            _ungroupedFields = _serializedFields
                .Where(f => f.GetCustomAttributes(typeof(GroupAttribute), true).Length == 0);
        }

        private List<SerializedProperty> FindSerializedProperties(List<SerializedProperty> properties)
        {
            if (_serializedProperties == null)
                _serializedProperties = new List<SerializedProperty>();
                
            properties.Clear();

            using (var iterator = serializedObject.GetIterator())
            {
                if (iterator.NextVisible(true))
                {
                    do
                    {
                        properties.Add(serializedObject.FindProperty(iterator.name));
                    } while (iterator.NextVisible(false));
                }
            }

            return properties;
        }

        private void BeginGroup(string groupName)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);

            if (!string.IsNullOrEmpty(groupName))
                EditorGUILayout.LabelField(groupName, EditorStyles.boldLabel);
        }

        private void EndGroup()
        {
            EditorGUILayout.EndVertical();
        }
    }
}
