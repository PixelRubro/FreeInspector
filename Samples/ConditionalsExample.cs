using UnityEngine;
using PixelSparkStudio.Inspector;

namespace PixelSparkStudio.Inspector.Samples
{
    public class ConditionalsExample : MonoBehaviour
    {
        [Separator]

        [SerializeField]
        [NotNull]
        private GameObject _cantBeNull;
        
        [Separator]

        [SerializeField]
        [ReadOnly]
        private float _readOnlyValue;

        [Separator]

        [SerializeField]
        private bool _showIfTest;

        [SerializeField]
        [ShowIf(nameof(_showIfTest))]
        private int _showMe;

        [Separator]

        [SerializeField]
        private bool _showInfoHelpboxIfTest;

        [SerializeField]
        [ShowHelpBoxIf(nameof(_showInfoHelpboxIfTest), "This is a help text.")]
        private int _showHelpboxInfo;

        [Separator]

        [SerializeField]
        private bool _showWarningHelpboxIfTest;

        [SerializeField]
        [ShowHelpBoxIf(nameof(_showWarningHelpboxIfTest), "This is a warning text.", EMessageType.Warning)]
        private int _showHelpboxWarning;

        [Separator]

        [SerializeField]
        private bool _showErrorHelpboxIfTest;

        [SerializeField]
        [ShowHelpBoxIf(nameof(_showErrorHelpboxIfTest), "This is a warning text.", EMessageType.Error)]
        private int _showHelpboxError;

        [Separator]
        
        [SerializeField]
        private bool _hideIfTest = true;

        [SerializeField]
        [HideIf(nameof(_hideIfTest))]
        private int _hideMe;

        [Separator]

        [SerializeField]
        private bool _hideInfoHelpboxIfTest = true;

        [SerializeField]
        [HideHelpBoxIf(nameof(_hideInfoHelpboxIfTest), "This is a help text.")]
        private int _helpboxInfo;

        [Separator]

        [SerializeField]
        private bool _hideWarningHelpboxIfTest = true;

        [SerializeField]
        [HideHelpBoxIf(nameof(_hideWarningHelpboxIfTest), "This is a warning text.", EMessageType.Warning)]
        private int _helpboxWarning;

        [Separator]

        [SerializeField]
        private bool _hideErrorHelpboxIfTest = true;

        [SerializeField]
        [HideHelpBoxIf(nameof(_hideErrorHelpboxIfTest), "This is a warning text.", EMessageType.Error)]
        private int _helpboxError;
        
        [Separator]

        [SerializeField]
        private bool _enableIfTest = true;

        [SerializeField]
        [EnableIf(nameof(_enableIfTest))]
        private int _enableMe;
        
        [Separator]

        [SerializeField]
        private bool _disableIfTest = true;

        [SerializeField]
        [DisableIf(nameof(_disableIfTest))]
        private int _disableMe;
        
        [Separator]

        [SerializeField]
        [ShowInPlayMode]
        private float _showInPlayMode;
        
        [Separator]

        [SerializeField]
        [HideInPlayMode]
        private float _hideInPlayMode;
        
        [Separator]

        [SerializeField]
        [EnableInPlayMode]
        private float _enableInPlayMode;
        
        [Separator]

        [SerializeField]
        [DisableInPlayMode]
        private float _disableInPlayMode;
        
        [Separator]

        [ShowProperty]
        public float PropertyExample => 15f;

        [ShowNonSerializedField]
        private float _unserializedField;

        private void BogusMethod()
        {
            if (_hideIfTest)
            {
                _hideIfTest = !_hideIfTest;
            }

            if (_hideInfoHelpboxIfTest)
            {
                _hideInfoHelpboxIfTest = !_hideInfoHelpboxIfTest;
            }

            if (_hideWarningHelpboxIfTest)
            {
                _hideWarningHelpboxIfTest = !_hideWarningHelpboxIfTest;
            }

            if (_hideErrorHelpboxIfTest)
            {
                _hideErrorHelpboxIfTest = !_hideErrorHelpboxIfTest;
            }

            if (_enableIfTest)
            {
                _enableIfTest = !_enableIfTest;
            }

            if (_disableIfTest)
            {
                _disableIfTest = !_disableIfTest;
            }
        }
    }
}
