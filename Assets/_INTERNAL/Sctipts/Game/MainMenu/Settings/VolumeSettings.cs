using Core.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MainMenu.Settings
{
    public class VolumeSettings
    {
        private Slider _sfxSlider;
        private Slider _musicSlider;

        public VolumeSettings(Slider sfxSlider, Slider musicSlider)
        {
            _sfxSlider = sfxSlider;
            _musicSlider = musicSlider;
        }

        public void SubscribeOnSFXEvent(UnityAction<float> action)
        {
            _sfxSlider.onValueChanged?.AddListener(action);
        }

        public void SubscribeOnMusicEvent(UnityAction<float> action)
        {
            _musicSlider.onValueChanged?.AddListener(action);
        }

        public void UnsubscribeFromSFXEvent(UnityAction<float> action)
        {
            _sfxSlider.onValueChanged?.RemoveListener(action);
        }

        public void UnsubscribeFromMusicEvent(UnityAction<float> action)
        {
            _musicSlider.onValueChanged?.RemoveListener(action);
        }

        public void LoadSlidersValues()
        {
            float sfx = PlayerPrefs.GetFloat(PlayerPrefsConsts.SFX_VOLUME, 1f);
            float music = PlayerPrefs.GetFloat(PlayerPrefsConsts.MUSIC_VOLUME, 1f);

            _musicSlider.value = music;
            _sfxSlider.value = sfx;
        }
    }
}