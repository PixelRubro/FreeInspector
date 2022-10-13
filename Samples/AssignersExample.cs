using UnityEngine;
using SoftBoiledGames.Inspector;

namespace SoftBoiledGames.Inspector.Samples
{
    public class AssignersExample : MonoBehaviour
    {
        [Separator]

        [SerializeField]
        [Prefab]
        private GameObject _prefabOnly;
        
        [Separator]

        [SerializeField]
        [Tag]
        private string _tag;
        
        [Separator]

        [SerializeField]
        [Input]
        private string _inputButton;
        
        [Separator]

        [SerializeField]
        [Dropdown(nameof(_dropdownValues))]
        private string _dropdown;

        private string[] _dropdownValues = new string[3]{"Value1", "Value2", "Value3"};

        [Separator]
        [SerializeField]
        [EnumFlags]
        private ExampleEnum _enumFlags;

        [Separator]
        [SerializeField]
        [Scene]
        private string _demoScene;

        public enum ExampleEnum
        {
            EnumValue1, EnumValue2, EnumValue3
        }
    }
}
