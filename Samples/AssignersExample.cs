using UnityEngine;
using VermillionVanguard.Inspector;

namespace VermillionVanguard.Inspector.Samples
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
        [Dropdown(nameof(_damageTypes))]
        private string _damageType;

        private string[] _damageTypes = new string[2]{"Physical", "Magical"};

        [Separator]
        [SerializeField]
        [EnumFlags]
        private Direction _enumFlags;

        [Separator]
        [SerializeField]
        [Scene]
        private string _demoScene;

        public enum Direction
        {
            None = 0,
            Up = 1 << 0,
            Right = 1 << 1,
            Left = 1 << 2,
            Down = 1 << 3
        }
    }
}
