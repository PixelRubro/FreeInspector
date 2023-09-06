using UnityEngine;
using VermillionVanguard.Inspector;

namespace VermillionVanguard.Inspector.Samples
{
    public class DecorationsExample : MonoBehaviour
    {
        [SerializeField]
        [Group("Group's name")]
        private bool _groupItem1;

        [SerializeField]
        [Group("Group's name")]
        private bool _groupItem2;

        [SerializeField]
        [Group("Group's name")]
        private bool _groupItem3;
        
        [SerializeField]
        [Foldout("Foldout's name")]
        private bool _foldoutItem1;

        [SerializeField]
        [Foldout("Foldout's name")]
        private bool _foldoutItem2;

        [SerializeField]
        [Foldout("Foldout's name")]
        private bool _foldoutItem3;

        [Separator]

        [SerializeField]
        [HelpBox("Info text inside a help box.")]
        private int _helpboxInfo;

        [Separator]

        [SerializeField]
        [HelpBox("Warning text inside a help box.", EMessageType.Warning)]
        private int _helpboxWarning;

        [Separator]
        [SerializeField]
        [HelpBox("Error text inside a help box.", EMessageType.Error)]
        private int _helpboxError;

        [Separator]

        [SerializeField]
        [LeftToggle]
        private bool _checkboxOnTheLeft;

        [Separator]

        [SerializeField]
        [Label("This label was changed")]
        private bool _changedLabel;

        [Separator]

        [SerializeField]
        private bool _fieldWithNoLabelBelow;

        [SerializeField]
        [HideLabel]
        private bool _noLabel;

        [Separator]

        [SerializeField]
        private bool _separatorBelow;

        [Separator]

        [SerializeField]
        private bool _separatorAbove;

        [Separator]

        [SerializeField]
        [ProgressBar("Progress Bar Example")]
        private float _progressBarExample = 0.75f;

        [Separator]

        [SerializeField]
        [Password]
        private string _password = "12345678";

        [Separator]

        [SerializeField]
        [Highlight(EColor.DarkViolet)]
        private float _highlightMe;

        [Separator]

        [SerializeField]
        [AssetPreview]
        private Sprite _spriteWithPreview;

        private void DummyMethod()
        {
            if (_progressBarExample > 0f)
            {
                _progressBarExample = 0f;
            }

            if (_password == "")
            {
                _password = "";
            }
        }
    }
}
