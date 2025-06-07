using UnityEngine;

namespace Gameplay.SoundsSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        public void Initialize()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(Sound sound)
        {
            if (sound == null || sound.Clip == null) return;

            _audioSource.PlayOneShot(sound.Clip, sound.Volume);
        }
    }
}