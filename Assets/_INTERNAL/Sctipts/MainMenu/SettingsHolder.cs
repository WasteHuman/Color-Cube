using Gameplay.SoundsSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class SettingsHolder : MonoBehaviour
    {
        [Header("Music")]
        [SerializeField] private List<Sprite> _musicIcons;

        [Space(5), Header("Sounds")]
        [SerializeField] private List<Sprite> _soundIcons;

        [Space(5), Header("SettingsUI panel")]
        [SerializeField] private SettingsUI _settingsPanel;

        [Space(5), Header("Buttons")]
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openBGSettings;

        private AudioSystem _audioSystem;

        private void OnEnable()
        {
            ButtonsSubscribe();
            AudioSystemInitialize();
        }

        private void OnDisable()
        {
            ButtonsUnsubscribe();
        }

        private void AudioSystemInitialize()
        {
            _audioSystem = AudioSystem.Instance;
            _settingsPanel.SetAudioSystem(_audioSystem);
        }

        private void ButtonsSubscribe()
        {
            _openButton.onClick.AddListener(()=> _settingsPanel.Open(_openButton));
            _closeButton.onClick.AddListener(() => _settingsPanel.Close(_openButton));
        }

        private void ButtonsUnsubscribe()
        {
            _openButton.onClick.RemoveListener(() => _settingsPanel.Open(_openButton));
            _closeButton.onClick.RemoveListener(() => _settingsPanel.Close(_openButton));
        }
    }
}