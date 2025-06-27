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

        private void OnEnable() => _button.onClick.AddListener(OnClosed);
        private void OnDisable() => _button.onClick.RemoveListener(OnClosed);

        protected override void OnOpened() { }
        protected override void OnClosed() { }
    }
}