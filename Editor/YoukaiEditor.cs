using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using YoukaiFox.UnityExtensions;
using YoukaiFox.Inspector.Extensions;
using YoukaiFox.Inspector.Utilities;
using YoukaiFox.Inspector.CustomStructures;
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

        private IEnumerable<SerializedProperty> _groupFields;
        private IEnumerable<IGrouping<string, SerializedProperty>> _boxGroupedFields;
        private IEnumerable<IGrouping<string, SerializedProperty>> _foldoutGroupedFields;
        private IEnumerable<SerializedProperty> _foldoutGroupFields;
        private IEnumerable<SerializedProperty> _reorderableListProperties;
        private IEnumerable<SerializedProperty> _ungroupedFields;
        private List<InspectorField> _fields = new List<InspectorField>();
        // private HashSet<string> _drawnGroups = new HashSet<string>();
        // private IEnumerable<FieldInfo> _nonSerializedFields;
        // private IEnumerable<FieldInfo> _serializedFields;
        // private IEnumerable<PropertyInfo> _nativeProperties;
        private IEnumerable<MethodInfo> _methods;
        private IEnumerable<MethodInfo> _methodsNoArguments;
        private IEnumerable<MethodInfo> _methodsWithArguments;

        #endregion

        private List<SerializedProperty> _serializedProperties;
        private Dictionary<string, SavedBool> _foldoutStates = new Dictionary<string, SavedBool>();
        private HashSet<ReorderableList> _reorderableLists = new HashSet<ReorderableList>();
        private int _reorderableListIndex;
        private bool _drawSeparators = false;
        private bool _drawFieldsInOrder = false;
        private bool _drawLooseFieldsFirst = true;
        private bool _groupLooseFields = false;

        #endregion
        
        private void OnEnable() 
        {
            _serializedProperties = new List<SerializedProperty>();
            _serializedProperties = FindSerializedProperties(_serializedProperties);
            FindAttributes();
        }

        private void OnDisable() 
        {
            _reorderableListIndex = 0;
        }

        public override void OnInspectorGUI() 
        {
            serializedObject.Update();
            _serializedProperties = FindSerializedProperties(_serializedProperties);
            // CollectPropertiesInfo();
            DrawScriptField();
            DrawFields();
            serializedObject.ApplyModifiedProperties();
        }

        // public override bool RequiresConstantRepaint()
        // {
        //     return false;
        // }

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

        private void DrawFields()
        {
            if (_drawFieldsInOrder)
                DrawFieldsInOrder();
            else
                DrawFieldsBySections();
        }

        private void DrawFieldsInOrder()
        {

            // _drawnGroups.Clear();

            // foreach (var p in _fields)
            // {
            //     if (p.Property.IsVisible())
            //         continue;

            //     switch (p.GroupingType)
            //     {
            //         case EGroupingType.None:
            //             if (Event.current.type == EventType.Repaint)
            //                 EditorGUILayout.PropertyField(p.Property);
            //             break;
            //         case EGroupingType.BoxGroup:
            //             if (_drawnGroups.Contains(p.GroupingName))
            //                 continue;

            //             var group = GetBoxGroupWithName(p.GroupingName);
            //             DrawGroup(group);
            //             _drawnGroups.Add(p.GroupingName);
            //             break;
            //         case EGroupingType.Foldout:
            //             if (_drawnGroups.Contains(p.GroupingName))
            //                 continue;

            //             var foldout = GetFoldoutGroupWithName(p.GroupingName);
            //             DrawFoldoutGroup(foldout);
            //             _drawnGroups.Add(p.GroupingName);
            //             break;
            //         default:
            //             throw new System.ArgumentOutOfRangeException();
            //     }
            // }
        }

        private void DrawFieldsBySections()
        {
            if (_drawLooseFieldsFirst)
                DrawUngroupedFields();

            DrawReoderableLists();
            DrawAllGroups();
            DrawAllFoldoutGroups();
            DrawButtons();

            if (!_drawLooseFieldsFirst)
                DrawUngroupedFields();
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

        private void DrawGroup(IGrouping<string, SerializedProperty> group)
        {
            serializedObject.Update();
            BeginGroup(group.Key);

            foreach (var field in group)
            {
                if (field.IsVisible())
                {
                    using (var scope = new EditorGUILayout.VerticalScope(EditorUtil.GroupBackgroundStyle()))
                    {
                        EditorGUILayout.PropertyField(field, true);
                    };
                }
            }

            EndGroup();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawAllGroups()
        {
            DrawSeparatorLine(Color.gray);

            foreach (var group in _boxGroupedFields)
            {
                DrawGroup(group);
            }
        }

        private void DrawFoldoutGroup(IGrouping<string, SerializedProperty> foldoutGroup)
        {
            serializedObject.Update();

            var key = foldoutGroup.Key;

            if (!_foldoutStates.ContainsKey(key))
            {
                var savedBool = new SavedBool($"{target.GetInstanceID()}.{key}", false);
                _foldoutStates.Add(key, savedBool);
            }

            _foldoutStates[key].Value = EditorGUILayout.Foldout(_foldoutStates[key].Value, key, EditorUtil.FoldoutStyle());

            if (_foldoutStates[key].Value)
            {
                foreach (var field in foldoutGroup)
                {
                    if (field.IsVisible())
                    {
                        using (new EditorGUI.IndentLevelScope(EditorUtil.FoldoutIndent))
                        {
                            EditorGUILayout.PropertyField(field, true);
                        }
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawAllFoldoutGroups()
        {
            DrawSeparatorLine(Color.gray);

            foreach (var foldoutGroup in _foldoutGroupedFields)
            {
                DrawFoldoutGroup(foldoutGroup);
            }
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

            _groupFields = _serializedProperties
                .Where(p => p.GetAttribute<GroupAttribute>() != null);
                // .GroupBy(f => f.Name);

            _boxGroupedFields = _groupFields.GroupBy(f => f.GetAttribute<GroupAttribute>().Name);

            _foldoutGroupFields = _serializedProperties
                .Where(p => p.GetAttribute<FoldoutAttribute>() != null);

            _foldoutGroupedFields = _foldoutGroupFields.GroupBy(f => f.GetAttribute<FoldoutAttribute>().Name);

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
                
            // if (properties.Count == 0)
                // return properties;
                
            properties.Clear();

            try
            {
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
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }

            return properties;
        }

        private void CollectPropertiesInfo()
        {
            for (int i = 0; i < _serializedProperties.Count; i++)
            {
                var order = i;
                var groupingType = _serializedProperties[i].GetGroupingType(out string groupingName);
                var field = new InspectorField(order, groupingType, groupingName, _serializedProperties[i]);
                _fields.Add(field);
            }
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

        private IGrouping<string, SerializedProperty> GetBoxGroupWithName(string name)
        {
            return _boxGroupedFields.Where(g => g.Key.Equals(name)).SingleOrDefault();
        }

        private IGrouping<string, SerializedProperty> GetFoldoutGroupWithName(string name)
        {
            return _foldoutGroupedFields.Where(g => g.Key.Equals(name)).SingleOrDefault();
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
