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
        private IEnumerable<FieldInfo> _nonSerializedFields;
        private IEnumerable<PropertyInfo> _properties;
        private IEnumerable<MethodInfo> _methods;
        private IEnumerable<MethodInfo> _methodsNoArguments;
        private IEnumerable<MethodInfo> _methodsWithArguments;
        
        private void OnEnable() 
        {
            // _nonSerializedFields = target.
            //     GetAllFields(f => f.GetCustomAttributes(typeof(ShowNonSerializedFieldAttribute), true).Length > 0);

            // _properties = target.
            //     GetAllProperties(p => p.GetCustomAttributes(typeof(ShowPropertyAttribute), true).Length > 0);

            _methods = target.
                GetAllMethods(m => m.GetCustomAttributes(typeof(ButtonAttribute), true).Length > 0);

            _methodsNoArguments = _methods.Where(m => m.GetParameters().Length == 0);
            _methodsWithArguments = _methods.Where(m => m.GetParameters().Length > 0);
        }

        public override void OnInspectorGUI() 
        {
            DrawDefaultInspector();
            DrawButtons();
        }

        private void DrawButtons()
        {
            if (!_methods.Any())
                return;

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
    }
}
