using UnityEngine;
using UnityEditor;
using System.Reflection;
using SoftBoiledGames.Inspector.Extensions;

namespace SoftBoiledGames.Inspector
{
    [CustomPropertyDrawer(typeof(AutoSetAttribute))]
    public class AutoSetAttributeDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var autosetAttribute = attribute as AutoSetAttribute;
            var component = FindTargetComponent(property, autosetAttribute.AlsoSearchInChildren);

            if ((component != null) && (property.objectReferenceValue == null))
            {
                property.objectReferenceValue = component;
                return;
            }

            if (component == null)
                DrawErrorMessage(position, "Couldn't find a suitable component!");
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var autosetAttribute = attribute as AutoSetAttribute;

            if (FindTargetComponent(property, autosetAttribute.AlsoSearchInChildren))
                return 0f;

            return base.GetPropertyHeight(property, label);
        }

        /// <summary>
        /// Takes a SerializedProperty and finds a local component that can be slotted into it.
        /// Local in this context means it's a component attached to the same GameObject.
        /// This could easily be changed to use GetComponentInParent/GetComponentInChildren
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private Component FindTargetComponent(SerializedProperty property, bool alsoSearchInChildren = false)
        {
            var root = property.serializedObject;

            if (root.targetObject is Component component)
            {
                var type = property.GetPropertyType();
                component = component.GetComponent(type);
                
                if ((component == null) && (alsoSearchInChildren))
                    component = component.GetComponentInChildren(type);

                return component;
            }

            return null;
        }
    }
}