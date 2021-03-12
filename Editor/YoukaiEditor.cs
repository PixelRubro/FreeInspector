using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using YoukaiFox.Inspector.Extensions;
using YoukaiFox.Inspector.Utilities;
using UnityEditorInternal;

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
        private IEnumerable<SerializedProperty> _reorderableListProperties;
        private IEnumerable<SerializedProperty> _ungroupedFields;
        private IEnumerable<FieldInfo> _nonSerializedFields;
        private IEnumerable<FieldInfo> _serializedFields;
        private IEnumerable<PropertyInfo> _nativeProperties;
        private IEnumerable<MethodInfo> _methods;
        private IEnumerable<MethodInfo> _methodsNoArguments;
        private IEnumerable<MethodInfo> _methodsWithArguments;

        #endregion

        private List<SerializedProperty> _serializedProperties;
        private Dictionary<string, SavedBool> _foldoutStates = new Dictionary<string, SavedBool>();
        private HashSet<ReorderableList> _reorderableLists = new HashSet<ReorderableList>();
        private int _reorderableListIndex;
        private bool _drawSeparators = true;
        private bool _drawLooseFieldsFirst = true;
        private bool _groupLooseFields = false;

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
            _serializedProperties = FindSerializedProperties(_serializedProperties);
            DrawScriptField();

            if (_drawLooseFieldsFirst)
                DrawUngroupedFields();

            DrawReoderableLists();
            DrawGroups();
            DrawFoldoutGroups();
            DrawButtons();

            if (!_drawLooseFieldsFirst)
                DrawUngroupedFields();

            // DrawDefaultInspector();
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(serializedObject.targetObject);
            Repaint();
        }

        private void DrawScriptField()
        {
            foreach (var field in _ungroupedFields)
            {
			    if (field.name.Equals("m_Script", System.StringComparison.Ordinal))
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(field);
                    EditorGUI.EndDisabledGroup();
                    break;
                }
            }
        }

        private void DrawButtons()
        {
            if (!_methods.Any())
                return;

            serializedObject.Update();
            DrawSeparatorLine(Color.gray);

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
            DrawSeparatorLine(Color.gray);
            var visibleFields = _groupedFields.Where(f => f.IsVisible());
            var groupedFields = visibleFields.GroupBy(f => f.GetAttribute<GroupAttribute>().Name);

            foreach (var group in groupedFields)
            {
                BeginGroup(group.Key);

                foreach (var prop in group)
                {
                    using (var scope = new EditorGUILayout.VerticalScope(EditorUtil.GroupBackgroundStyle()))
                    {
                        EditorGUILayout.PropertyField(prop, true);
                    };
                }

                EndGroup();
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawFoldoutGroups()
        {
            serializedObject.Update();
            DrawSeparatorLine(Color.gray);
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
                        using (new EditorGUI.IndentLevelScope(1))
                        {
                            EditorGUILayout.PropertyField(prop, true);
                        }
                    }
                }
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawReoderableLists()
        {
            EditorGUILayout.Space();
            DrawSeparatorLine(Color.gray);

            foreach (var list in _reorderableLists)
            {
                list.DoLayoutList();
            }

            _reorderableListIndex = -1;
        }

        private void DrawUngroupedFields()
        {
            serializedObject.Update();
            DrawSeparatorLine(Color.gray);
            var visibleFields = _ungroupedFields.Where(f => f.IsVisible());
            bool skippedScriptField = false;

            if (_groupLooseFields)
                BeginGroup("Remaining fields");

            foreach (var field in visibleFields)
            {
			    if ((!skippedScriptField) && (field.name.Equals("m_Script", System.StringComparison.Ordinal)))
                {
                    skippedScriptField = true;
                    continue;
                }

                if (_groupLooseFields)
                {
                    using (var scope = new EditorGUILayout.VerticalScope(EditorUtil.UngroupedFieldsStyle()))
                    {
                        EditorGUILayout.PropertyField(field);
                    };
                }
                else
                    EditorGUILayout.PropertyField(field);
            }

            if (_groupLooseFields)
                EndGroup();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSeparatorLine(Color color)
        {
            if (!_drawSeparators)
                return;

            var lineStyle = EditorUtil.HorizontalLineStyle();
            var guiColor = GUI.color;
            GUI.color = color;
            GUILayout.Box(GUIContent.none, lineStyle);
            GUI.color = guiColor;
        }

        private void FindAttributes()
        {
            // var temp = _reorderableListProperties.ToList();
            // temp.Clear();
            // _reorderableListProperties = temp;
            // _serializedFields = target.
            //     GetAllFields(f => f.GetCustomAttributes(typeof(SerializeField), true).Length > 0);

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
                .Where(p => p.GetAttribute<FoldoutAttribute>() != null);

            _reorderableListProperties = _serializedProperties
                .Where(p => p.GetAttribute<ReorderableListAttribute>() != null);

            foreach (var list in _reorderableListProperties)
            {
                var reorderableList = new ReorderableList(serializedObject, list, true, true, true, true)
                    {
                        drawHeaderCallback = DrawListHeader,
                        drawElementCallback = DrawListElement
                    };

                _reorderableLists.Add(reorderableList);
            }

            _ungroupedFields = _serializedProperties
                .Where(p => p.GetAttribute<GroupAttribute>() == null)
                .Where(p => p.GetAttribute<FoldoutAttribute>() == null)
                .Where(p => p.GetAttribute<ReorderableListAttribute>() == null);
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

        private void DrawListHeader(Rect rect) 
        {
            var list = GetCurrentReordableList();
            var name = ObjectNames.NicifyVariableName(list.name);
            GUI.Label(rect, name);
        }

        private void DrawListElement(Rect rect, int index, bool isActive, bool isFocused) 
        {
            serializedObject.Update();
            var list = GetCurrentReordableList();
            var item = list.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, item);
            serializedObject.ApplyModifiedProperties();
        }

        private SerializedProperty GetCurrentReordableList()
        {
            var lists = _reorderableListProperties.ToArray();

            if (lists.Length == 0)
                return lists[0];

            // Debug.Log("Currently, only ONE reorderable list by inspector is allowed.");
            return lists[0];
            throw new System.NotImplementedException();
        }

        private void BeginGroup(string groupName)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            var labelStyle = EditorStyles.boldLabel;
            // labelStyle.alignment = TextAnchor.MiddleCenter;

            if (!string.IsNullOrEmpty(groupName))
                EditorGUILayout.LabelField(groupName, labelStyle);
        }

        private void EndGroup()
        {
            EditorGUILayout.EndVertical();
        }
    }
}