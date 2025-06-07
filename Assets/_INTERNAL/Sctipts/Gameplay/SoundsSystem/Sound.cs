using UnityEngine;

namespace Gameplay.SoundsSystem
{
    [CreateAssetMenu(menuName = "Sound system/Sound", fileName = "Sound")]
    public class Sound : ScriptableObject
    {
        [Tooltip("Уникальный ID звука")]
        [SerializeField] private SoundID _uniqID;

        [Space(10), Header("Sound Info")]
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _volume;
        [SerializeField] private float _pitch;

        public SoundID UniqID => _uniqID;
        public AudioClip Clip => _clip;
        public float Volume => _volume;
        public float Pitch => _pitch; 
    }

    [System.Serializable]
    public enum SoundID
    {
        Click, RightChosen, Lose
    }
}