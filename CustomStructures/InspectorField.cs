using System;
using UnityEditor;
using UnityEngine;

namespace YoukaiFox.Inspector.CustomStructures
{
    public enum EGroupingType
    {
        None, BoxGroup, Foldout
    }

    public class InspectorField : IComparable<InspectorField>
    {
        public int Order;
        public EGroupingType GroupingType;
        public string GroupingName;
        public SerializedProperty Property;

        public InspectorField(int order, SerializedProperty property)
        {
            Order = order;
            GroupingType = EGroupingType.None;
            GroupingName = "none";
            Property = property;
        }

        public InspectorField(int order, EGroupingType groupingType, string groupingName, SerializedProperty property)
        {
            Order = order;
            GroupingType = groupingType;
            GroupingName = groupingName;
            Property = property;
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
