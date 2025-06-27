using Gameplay.Player;
using Gameplay.SoundsSystem;
using MainMenu.Background;
using MainMenu.Settings;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [Header("Setting sliders")]
        [SerializeField] private Slider _soundsSlider;
        [SerializeField] private Slider _musicSlider;

        private readonly float _delayBeforeClosing = 0.8f;

        private VolumeSettings _volumeSettings;
        private AudioSystem _audioSystem;

        private bool _isOpened = false;

        private void OnEnable()
        {
            _volumeSettings ??= new(_soundsSlider, _musicSlider);

            _audioSystem.SubscribeOnEvents();
            _volumeSettings.SubscribeOnSFXEvent(OnSFXVolumeChanged);
            _volumeSettings.SubscribeOnMusicEvent(OnMusicVolumeChanged);

            _volumeSettings.LoadSlidersValues();
        }

        private void OnDisable()
        {
            PlayerPrefs.Save();

            _volumeSettings.UnsubscribeFromSFXEvent(OnSFXVolumeChanged);
            _volumeSettings.UnsubscribeFromMusicEvent(OnMusicVolumeChanged);
            _audioSystem.UnsubscribeFromEvents();
        }

        public void SetAudioSystem(AudioSystem audioSystem)
        {
            _audioSystem = audioSystem;
        }

        public void Open(Button button)
        {
            gameObject.SetActive(true);
            _isOpened = true;
            button.interactable = false;

            _audioSystem.PlaySoundByID(SoundID.Click);

            _animator.SetBool("Opened", _isOpened);
        }

        public void Close(Button button)
        {
            _isOpened = false;

            _audioSystem.PlaySoundByID(SoundID.Click);

            _animator.SetBool("Opened", _isOpened);

            StartCoroutine(ClosingDelay(button));
        }

        private void OnSFXVolumeChanged(float value)
        {
            VolumeEvents.ChangeSFXVolume(value);
            PlayerPrefs.SetFloat(PlayerPrefsConsts.SFX_VOLUME, value);
        }

        private void OnMusicVolumeChanged(float value)
        {
            VolumeEvents.ChangeMusicVolume(value);
            PlayerPrefs.SetFloat(PlayerPrefsConsts.MUSIC_VOLUME, value);
        }

        private IEnumerator ClosingDelay(Button button)
        {
            yield return new WaitForSeconds(_delayBeforeClosing);

            gameObject.SetActive(false);
            button.interactable = true;
        }
    }
}