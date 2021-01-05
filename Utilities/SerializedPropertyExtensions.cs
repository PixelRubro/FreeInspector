using UnityEditor;
using System;
using System.Reflection;

namespace YoukaiFox.Inspector
{
    public static class SerializedPropertyExtensions
    {
        #region Extensions

        // Author: github.com/lordofduct
        /// <summary>
        /// Gets the object the property represents.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static object GetObject(this SerializedProperty self)
        {
            if (self == null) return null;

            var path = self.propertyPath.Replace(".Array.data[", "[");
            object obj = self.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }

        // Author: github.com/arimger
        public static SerializedProperty GetSibiling(this SerializedProperty self, string propertyPath)
        {
            return self.depth == 0 || self.GetParent() == null
                ? self.serializedObject.FindProperty(propertyPath)
                : self.GetParent().FindPropertyRelative(propertyPath);
        }

        // Author: github.com/arimger
        public static SerializedProperty GetParent(this SerializedProperty self)
        {
            if (self.depth == 0)
            {
                return null;
            }

            var path = self.propertyPath.Replace(".Array.data[", "[");
            var elements = path.Split('.');

            SerializedProperty parent = null;

            for (var i = 0; i < elements.Length - 1; i++)
            {
                var element = elements[i];
                var index = -1;
                if (element.Contains("["))
                {
                    index = Convert.ToInt32(element
                        .Substring(element.IndexOf("[", StringComparison.Ordinal))
                        .Replace("[", "").Replace("]", ""));
                    element = element
                        .Substring(0, element.IndexOf("[", StringComparison.Ordinal));
                }

                parent = i == 0 ? self.serializedObject.FindProperty(element) : parent.FindPropertyRelative(element);

                if (index >= 0) parent = parent.GetArrayElementAtIndex(index);
            }

            return parent;
        }

        #endregion

        #region Helper methods

        // Author: github.com/lordofduct
        private static object GetValue_Imp(object obj, string name)
        {
            if (obj == null)
                return null;
            var type = obj.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(obj);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(obj, null);

                type = type.BaseType;
            }
            return null;
        }

        // Author: github.com/lordofduct
        private static object GetValue_Imp(object obj, string name, int index)
        {
            var enumerable = GetValue_Imp(obj, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }

        #endregion
    }
}
