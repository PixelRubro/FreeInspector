#if UNITY_EDITOR
using UnityEditor;

namespace PixelRouge.Inspector.CustomStructures
{
	public class SavedBool
	{
		private bool _value;
		private string _name;

		public bool Value
		{
			get
			{
				return _value;
			}
			set
			{
				if (_value == value)
				{
					return;
				}

				_value = value;
				EditorPrefs.SetBool(_name, value);
			}
		}

		public SavedBool(string name, bool value)
		{
			_name = name;
			_value = EditorPrefs.GetBool(name, value);
		}
	}
}
#endif