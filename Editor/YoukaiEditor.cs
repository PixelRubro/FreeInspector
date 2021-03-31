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

        #region Attribute info storing

        private IEnumerable<SerializedProperty> _groupFields;
        private IEnumerable<IGrouping<string, SerializedProperty>> _boxGroupedFields;
        private IEnumerable<IGrouping<string, SerializedProperty>> _foldoutGroupedFields;
        private IEnumerable<SerializedProperty> _foldoutGroupFields;
        private IEnumerable<SerializedProperty> _reorderableListProperties;
        private IEnumerable<SerializedProperty> _ungroupedFields;
        private IEnumerable<FieldInfo> _nonSerializedFields;
        private IEnumerable<PropertyInfo> _nativeProperties;
        private IEnumerable<MethodInfo> _methods;
        private IEnumerable<MethodInfo> _methodsNoArguments;
        private IEnumerable<MethodInfo> _methodsWithArguments;
        private List<SerializedProperty> _serializedProperties = new List<SerializedProperty>();
        private Dictionary<string, SavedBool> _foldoutStates = new Dictionary<string, SavedBool>();
        private HashSet<ReorderableList> _reorderableLists = new HashSet<ReorderableList>();

        #endregion

        #region Settings

        private bool _drawUngroupedFieldsFirst = true;

        #endregion

        #endregion
        
        #region Unity events

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
            DrawFields();
            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Drawing

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
            DrawScriptField();
            DrawFieldsBySections();
        }

        private void DrawFieldsBySections()
        {
            if (_drawUngroupedFieldsFirst)
                DrawUngroupedFields();

            DrawReoderableLists();
            DrawAllGroups();
            DrawAllFoldoutGroups();
            DrawButtons();

            if (!_drawUngroupedFieldsFirst)
                DrawUngroupedFields();

            DrawNonSerializedProperties();
        }

        private void DrawButtons()
        {
            if (!_methods.Any())
                return;

            foreach (var method in _methods)
            {
                var buttonAttribute = Attribute.GetCustomAttribute(method, typeof(ButtonAttribute)) as ButtonAttribute;

                if (buttonAttribute != null)
                    DrawButton(buttonAttribute, method);
            }
        }

        private void DrawButton(ButtonAttribute buttonAttribute, MethodInfo method)
        {
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
                return;

            var arguments = buttonAttribute.Arguments;

            if (buttonAttribute.Arguments == null)
                arguments = method.GetParameters().Select(p => p.DefaultValue).ToArray();
            
            method.Invoke(serializedObject.targetObject, arguments);
            GUI.enabled = previousGuiStatus;
        }

        private void DrawGroup(IGrouping<string, SerializedProperty> group)
        {
            BeginGroup(group.Key);

            foreach (var field in group)
            {
                if (field.IsVisible())
                {
                    using (var scope = new EditorGUILayout.VerticalScope(EditorUtil.GroupBackgroundStyle()))
                    {
                        DrawSerializedProperty(field, true);
                    };
                }
            }

            EndGroup();
        }

        private void DrawAllGroups()
        {
            foreach (var group in _boxGroupedFields)
            {
                DrawGroup(group);
                EditorGUILayout.Space();
            }
        }

        private void DrawFoldoutGroup(IGrouping<string, SerializedProperty> foldoutGroup)
        {
            var key = foldoutGroup.Key;

            if (!_foldoutStates.ContainsKey(key))
            {
                var savedBool = new SavedBool($"{target.GetInstanceID()}.{key}", false);
                _foldoutStates.Add(key, savedBool);
            }

            _foldoutStates[key].Value = EditorGUILayout.Foldout(_foldoutStates[key].Value, key, EditorUtil.FoldoutStyle());

            if (!_foldoutStates[key].Value)
                return;

            foreach (var field in foldoutGroup)
            {
                if (field.IsVisible())
                {
                    using (new EditorGUI.IndentLevelScope(EditorUtil.FoldoutIndent))
                    {
                        DrawSerializedProperty(field, true);
                    }
                }
            }
        }

        private void DrawAllFoldoutGroups()
        {
            foreach (var foldoutGroup in _foldoutGroupedFields)
            {
                DrawFoldoutGroup(foldoutGroup);
            }
        }

        private void DrawReoderableLists()
        {
            foreach (var list in _reorderableLists)
            {
                list.DoLayoutList();
            }
        }

        private void DrawUngroupedFields()
        {
            bool skippedScriptField = false;

            foreach (var propertyField in _ungroupedFields)
            {
			    if ((!skippedScriptField) && (propertyField.name.Equals("m_Script", System.StringComparison.Ordinal)))
                {
                    skippedScriptField = true;
                    continue;
                }

                DrawSerializedProperty(propertyField);
            }
        }

        private void DrawNonSerializedProperties()
        {
            foreach (var field in _nonSerializedFields)
            {
                DrawNonSerializedProperty(serializedObject.targetObject, field);
            }

            foreach (var prop in _nativeProperties)
            {
                DrawNonSerializedProperty(serializedObject.targetObject, prop);
            }
        }

        private void DrawSeparatorLine(Color color)
        {
            var lineStyle = EditorUtil.HorizontalLineStyle();
            var guiColor = GUI.color;
            GUI.color = color;
            GUILayout.Box(GUIContent.none, lineStyle);
            GUI.color = guiColor;
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

        private void DrawSerializedProperty(SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property);
        }

        private void DrawSerializedProperty(SerializedProperty property, bool includeChildren)
        {
            EditorGUILayout.PropertyField(property, includeChildren);
        }

        private void DrawNonSerializedProperty(UnityEngine.Object targetObject, FieldInfo fieldInfo)
        {
            DrawDisabledProperty(fieldInfo.GetValue(targetObject), ObjectNames.NicifyVariableName(fieldInfo.Name));
        }

        private void DrawNonSerializedProperty(UnityEngine.Object targetObject, PropertyInfo propertyInfo)
        {
            DrawDisabledProperty(propertyInfo.GetValue(targetObject), ObjectNames.NicifyVariableName(propertyInfo.Name));
        }

        public static void DrawDisabledProperty(object value, string labelText)
        {
            using (new EditorGUI.DisabledScope(disabled: true))
            {
                Type valueType = value.GetType();

                if (valueType == typeof(bool))
                {
                    EditorGUILayout.ToggleLeft(labelText, (bool) value);
                }
                else if (valueType == typeof(int))
                {
                    EditorGUILayout.IntField(labelText, (int) value);
                }
                else if (valueType == typeof(long))
                {
                    EditorGUILayout.LongField(labelText, (long) value);
                }
                else if (valueType == typeof(float))
                {
                    EditorGUILayout.FloatField(labelText, (float) value);
                }
                else if (valueType == typeof(double))
                {
                    EditorGUILayout.DoubleField(labelText, (double) value);
                }
                else if (valueType == typeof(string))
                {
                    EditorGUILayout.TextField(labelText, (string) value);
                }
                else if (valueType == typeof(Vector2))
                {
                    EditorGUILayout.Vector2Field(labelText, (Vector2) value);
                }
                else if (valueType == typeof(Vector3))
                {
                    EditorGUILayout.Vector3Field(labelText, (Vector3) value);
                }
                else if (valueType == typeof(Vector4))
                {
                    EditorGUILayout.Vector4Field(labelText, (Vector4) value);
                }
                else if (valueType == typeof(Color))
                {
                    EditorGUILayout.ColorField(labelText, (Color) value);
                }
                else if (valueType == typeof(Bounds))
                {
                    EditorGUILayout.BoundsField(labelText, (Bounds) value);
                }
                else if (valueType == typeof(Rect))
                {
                    EditorGUILayout.RectField(labelText, (Rect) value);
                }
                else if (typeof(UnityEngine.Object).IsAssignableFrom(valueType))
                {
                    EditorGUILayout.ObjectField(labelText, (UnityEngine.Object) value, valueType, true);
                }
                else if (valueType.BaseType == typeof(Enum))
                {
                    EditorGUILayout.EnumPopup(labelText, (Enum) value);
                }
                else if (valueType.BaseType == typeof(System.Reflection.TypeInfo))
                {
                    EditorGUILayout.TextField(labelText, value.ToString());
                }
                else
                {
                    Debug.LogError("Couldn't draw the property.");
                }
            }
        }

        #endregion

        #region Property info collection

        private List<SerializedProperty> FindSerializedProperties(List<SerializedProperty> properties)
        {
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

        private void FindAttributes()
        {
            CollectUngroupedSerializedFieldsInfo();
            CollectNonSerializedFieldInfo();
            CollectMethodsInfo();
            CollectGroupInfo();
            CollectReorderableListInfo();
        }

        private void CollectUngroupedSerializedFieldsInfo()
        {
            _ungroupedFields = _serializedProperties
                .Where(p => p.GetAttribute<GroupAttribute>() == null)
                .Where(p => p.GetAttribute<FoldoutAttribute>() == null)
                .Where(p => p.GetAttribute<ReorderableListAttribute>() == null);
        }

        private void CollectNonSerializedFieldInfo()
        {
            _nonSerializedFields = target.
                GetAllFields(f => f.GetCustomAttributes(typeof(ShowNonSerializedFieldAttribute), true).Length > 0);

            _nativeProperties = target.
                GetAllProperties(p => p.GetCustomAttributes(typeof(ShowPropertyAttribute), true).Length > 0);
        }

        private void CollectMethodsInfo()
        {
            _methods = target.
                GetAllMethods(m => m.GetCustomAttributes(typeof(ButtonAttribute), true).Length > 0);

            _methodsNoArguments = _methods.Where(m => m.GetParameters().Length == 0);

            _methodsWithArguments = _methods.Where(m => m.GetParameters().Length > 0);
        }

        private void CollectGroupInfo()
        {
            _groupFields = _serializedProperties
                .Where(p => p.GetAttribute<GroupAttribute>() != null);

            _boxGroupedFields = _groupFields.GroupBy(f => f.GetAttribute<GroupAttribute>().Name);

            _foldoutGroupFields = _serializedProperties
                .Where(p => p.GetAttribute<FoldoutAttribute>() != null);

            _foldoutGroupedFields = _foldoutGroupFields.GroupBy(f => f.GetAttribute<FoldoutAttribute>().Name);
        }

        private void CollectReorderableListInfo()
        {
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
        }

        #endregion

        #region Grouping auxiliar methods

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

        #endregion

        #region Reorderable list auxiliar methods
        
        private SerializedProperty GetCurrentReordableList()
        {
            var lists = _reorderableListProperties.ToArray();

            if (lists.Length == 0)
                return lists[0];

            // Debug.Log("Currently, only ONE reorderable list by inspector is allowed.");
            return lists[0];
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
