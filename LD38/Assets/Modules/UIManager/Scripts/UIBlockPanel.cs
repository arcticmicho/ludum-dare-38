using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace GameModules
{
    public class UIBlockPanel : MonoBehaviour
    {
        private const float kAlphaFadeTime = 0.5f;

        [SerializeField]
        private Button _overlayButton;

        [SerializeField]
        private Image _blockImage;

        [SerializeField]
        private float _darkAlpha = 0.7f;

        private float _currentAlpha = 0;

        private bool _blockEnabled = false;
        private bool _darkEnabled = false;

        public bool BlockEnabled
        {
            get { return _blockEnabled; }
        }

        public bool DarkEnabled
        {
            get { return _darkEnabled; }
        }

        private void Awake()
        {
            _overlayButton.onClick.AddListener(OnOverlayPressed);
        }

        public void Setup(bool blockEnabled, bool darkEnabled)
        {
            _blockEnabled = blockEnabled;
            _darkEnabled = darkEnabled;

            if (_blockEnabled || _darkEnabled)
            {
                _blockImage.enabled = true;
            }

            float targetAlpha = (_darkEnabled) ? _darkAlpha : 0.0f;

            _blockImage.raycastTarget = _blockEnabled;

            UpdateAlphaStatus(targetAlpha);
        }

        private void UpdateAlphaStatus(float target)
        {
            if (LeanTween.isTweening(gameObject))
            {
                LeanTween.cancel(gameObject);
            }
            LeanTween.value(gameObject, _currentAlpha, target, kAlphaFadeTime).setOnUpdate(UpdateAlpha).setOnComplete(UpdateAlphaCompleted);
        }

        private void UpdateAlpha(float value)
        {
            _currentAlpha = value;
            Color newColor = _blockImage.color;
            newColor.a = _currentAlpha;
            _blockImage.color = newColor;
        }

        private void UpdateAlphaCompleted()
        {
            if (!_blockEnabled && !_darkEnabled)
            {
                _blockImage.enabled = false;
            }
        }

        private void OnOverlayPressed()
        {
            UIManager.Instance.PopupOverlayPressed();
        }
    }
}
