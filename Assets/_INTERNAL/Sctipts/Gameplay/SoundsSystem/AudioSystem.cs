using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.SoundsSystem
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
            _player.Initialize();

            SingletonInitialization();

            DictionaryInitialize(_sounds);
        }

        public void PlaySoundByID(SoundID soundID)
        {
            if (_soundMap.TryGetValue(soundID, out Sound sound))
            {
                _player.PlaySound(sound);
            }
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