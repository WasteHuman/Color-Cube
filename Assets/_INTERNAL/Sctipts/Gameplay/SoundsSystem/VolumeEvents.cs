using System;

namespace Gameplay.SoundsSystem
{
    public static class VolumeEvents
    {
        public static event Action<float> OnMusicVolumeChanged;
        public static event Action<float> OnSFXVolumeChanged;

        public static void ChangeMusicVolume(float volume) => OnMusicVolumeChanged?.Invoke(volume);
        public static void ChangeSFXVolume(float volume) => OnSFXVolumeChanged?.Invoke(volume);
    }
}