using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SimpleWindow : Window
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _text;

        [Space(10), Header("Close button")]
        [SerializeField] private Button _closeButton;

        private void OnEnable() => _closeButton.onClick.AddListener(OnClosed);
        private void OnDisable() => _closeButton.onClick.RemoveListener(OnClosed);

        protected override void OnOpened() { }
        protected override void OnClosed() { }
    }
}