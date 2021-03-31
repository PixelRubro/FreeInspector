using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace YoukaiFox.Inspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class DropdownAttribute : YoukaiAttribute
    {
        public string ValuesCollectionName;

        public DropdownAttribute(string valuesCollectionName)
        {
            ValuesCollectionName = valuesCollectionName;
        }
    }

	public interface IDropdownList : IEnumerable<KeyValuePair<string, object>>
	{
	}

	public class DropdownList<T> : IDropdownList
	{
		private List<KeyValuePair<string, object>> _values;

		public DropdownList()
		{
			_values = new List<KeyValuePair<string, object>>();
		}

		public void Add(string displayName, T value)
		{
			_values.Add(new KeyValuePair<string, object>(displayName, value));
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return _values.GetEnumerator();
		}

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static explicit operator DropdownList<object>(DropdownList<T> target)
		{
			DropdownList<object> result = new DropdownList<object>();
			foreach (var kvp in target)
			{
				result.Add(kvp.Key, kvp.Value);
			}

			return result;
		}
	}
}