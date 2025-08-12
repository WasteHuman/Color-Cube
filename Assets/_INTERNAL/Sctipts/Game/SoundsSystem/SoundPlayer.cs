using UnityEngine;
using UnityEngine.Audio;

namespace Game.SoundsSystem
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private string _sfxName = "SFXVolume";
        [SerializeField] private string _musicName = "MusicVolume";

        [Space(10), Header("Audio sources and Mixer")]
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        public void PlaySound(Sound sound)
        {
            if (sound == null || sound.Clip == null) return;

            _sfxSource.outputAudioMixerGroup = sound.MixerGroup;
            _sfxSource.PlayOneShot(sound.Clip, sound.Volume);
        }

        public void PlayMusic(Sound sound)
        {
            if (sound == null || sound.Clip == null) return;

            _musicSource.outputAudioMixerGroup = sound.MixerGroup;
            _musicSource.PlayOneShot(sound.Clip, sound.Volume);
        }

        public void ChangeSFXVolume(float volume)
        {
            volume = Mathf.Clamp(volume, 0.0001f, 1f);
            float dB = volume > 0 ? Mathf.Log10(volume) * 20f : -80f;
            _mixer.SetFloat(_sfxName, dB);
        }

        public void ChangeMusicVolume(float volume)
        {
            volume = Mathf.Clamp(volume, 0.0001f, 1f);
            float dB = volume > 0 ? Mathf.Log10(volume) * 20f : -80f;
            _mixer.SetFloat(_musicName, dB);
        }
    }
}