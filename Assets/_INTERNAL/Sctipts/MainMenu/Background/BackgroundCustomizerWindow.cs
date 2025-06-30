using Gameplay.SoundsSystem;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Background
{
    public class BackgroundCustomizerWindow : SimpleWindow
    {
        [Space(10), Header("Other buttons")]
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        [Space(10), Header("Customizer panel")]
        [SerializeField] private GameObject _customizerPanel;

        private AudioSystem _audioSystem;

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OpenCustomizer);
            _closeButton.onClick.AddListener(CloseCustomizer);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OpenCustomizer);
            _closeButton.onClick.RemoveListener(CloseCustomizer);
        }

        private void Awake()
        {
            _audioSystem = AudioSystem.Instance;
        }

        private void OpenCustomizer()
        {
            _audioSystem.PlaySoundByID(SoundID.Click);
            _customizerPanel.SetActive(true);
        }

        private void CloseCustomizer()
        {
            _audioSystem.PlaySoundByID(SoundID.Click);
            _customizerPanel.SetActive(false);
        }
    }
}