using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.SoundsSystem
{
    public class AudioSystem : MonoBehaviour
    {
        public static AudioSystem Instance { get; private set; }

        [SerializeField] private List<Sound> _sounds = new();

        private Dictionary<SoundID, Sound> _soundMap;
        private SoundPlayer _player;

        public void Initialize()
        {
            _player = GetComponent<SoundPlayer>();

            SingletonInitialization();

            DictionaryInitialize(_sounds);
        }

        public void SubscribeOnEvents()
        {
            VolumeEvents.OnSFXVolumeChanged += ChangeSFXVolume;
            VolumeEvents.OnMusicVolumeChanged += ChangeMusicVolume;

        }

        public void UnsubscribeFromEvents()
        {
            VolumeEvents.OnSFXVolumeChanged -= ChangeSFXVolume;
            VolumeEvents.OnMusicVolumeChanged -= ChangeMusicVolume;
        }

        public void PlaySoundByID(SoundID soundID)
        {
            if (_soundMap.TryGetValue(soundID, out Sound sound))
            {
                _player.PlaySound(sound);
            }
        }

        public void PlayMusicByID(SoundID soundID)
        {
            if (_soundMap.TryGetValue(soundID, out Sound sound))
            {
                _player.PlayMusic(sound);
            }
        }

        public void LoadVolumes()
        {
            float sfx = PlayerPrefs.GetFloat(PlayerPrefsConsts.SFX_VOLUME, 1f);
            float music = PlayerPrefs.GetFloat(PlayerPrefsConsts.MUSIC_VOLUME, 1f);

            ChangeSFXVolume(sfx);
            ChangeMusicVolume(music);
        }

        private void ChangeSFXVolume(float volume)
        {
            _player.ChangeSFXVolume(volume);
        }

        private void ChangeMusicVolume(float volume)
        {
            _player.ChangeMusicVolume(volume);
        }

        private void SingletonInitialization()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void DictionaryInitialize(List<Sound> sounds)
        {
            _soundMap = new();
            _soundMap = sounds.ToDictionary(s => s.UniqID);
        }
    }
}