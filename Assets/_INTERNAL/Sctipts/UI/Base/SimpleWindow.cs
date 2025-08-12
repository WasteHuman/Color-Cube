using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SimpleWindow : Window
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _text;

        [Space(10), Header("Button")]
        [SerializeField] private Button _button;

        private bool _initialized;

        private void Awake()
        {
            if (_button == null)
            {
                Debug.LogError($"[{nameof(SimpleWindow)}] Button is not assigned!", this);
                return;
            }

            _button.onClick.AddListener(OnButtonClick);
            _initialized = true;
        }

        private void OnDestroy()
        {
            if (_initialized)
                _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Close();
        }

        protected override void OnOpened() { }
        protected override void OnClosed() { }
    }
}