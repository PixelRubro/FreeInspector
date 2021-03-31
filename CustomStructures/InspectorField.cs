using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector.CustomStructures
{
    public enum EGroupingType
    {
        None, BoxGroup, Foldout
    }

    public enum EPropertySerializingType
    {
        Serialized, NonSerializedField, NativeProperty
    }

    public class InspectorField : IComparable<InspectorField>
    {
        public int Order;
        public EGroupingType GroupingType;
        public string GroupingName;
        public SerializedProperty Property;
        public EPropertySerializingType PropertySerializingType;
        public FieldInfo FieldInfo;
        public PropertyInfo PropertyInfo;

        public InspectorField(int order, SerializedProperty property)
        {
            Order = order;
            GroupingType = EGroupingType.None;
            GroupingName = "none";
            Property = property;
            PropertySerializingType = EPropertySerializingType.Serialized;
        }

        public InspectorField(int order, EGroupingType groupingType, string groupingName, FieldInfo fieldInfo)
        {
            Order = order;
            GroupingType = groupingType;
            GroupingName = "none";
            FieldInfo = fieldInfo;
            PropertySerializingType = EPropertySerializingType.NonSerializedField;
        }

        public InspectorField(int order, EGroupingType groupingType, string groupingName, PropertyInfo propertyInfo)
        {
            Order = order;
            GroupingType = groupingType;
            GroupingName = "none";
            PropertyInfo = propertyInfo;
            PropertySerializingType = EPropertySerializingType.NativeProperty;
        }

        public InspectorField(int order, EGroupingType groupingType, string groupingName, SerializedProperty property)
        {
            Order = order;
            GroupingType = groupingType;
            GroupingName = groupingName;
            Property = property;
            PropertySerializingType = EPropertySerializingType.Serialized;
        }

        public int CompareTo(InspectorField other)
        {
            if (Order > other.Order)
                return 1;

            if (Order < other.Order)
                return -1;

            return 0;
        }
    }
}
