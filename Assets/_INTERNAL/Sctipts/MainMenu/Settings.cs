using Gameplay.SoundsSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private bool _isOpened = false;

        private readonly float _delayBeforeClosing = 0.8f;

        private AudioSystem _audioSystem;

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

        private IEnumerator ClosingDelay(Button button)
        {
            yield return new WaitForSeconds(_delayBeforeClosing);

            gameObject.SetActive(false);
            button.interactable = true;
        }
    }
}