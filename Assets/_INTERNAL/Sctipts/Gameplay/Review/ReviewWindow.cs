using Gameplay.SoundsSystem;
using GameWindows;
using UI;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Gameplay.Review
{
    public class ReviewWindow : SimpleWindow
    {
        [field: SerializeField] public ReviewYG ReviewYG { get; private set; }
        [field: SerializeField] public bool CanBeOpened {  get; private set; }
        [field: SerializeField] public int PlayerLoseCountMax { get; private set; }
        [field: SerializeField] public int PlayerLoseCount { get; private set; }

        [SerializeField] private float _windowOpenChance;
        [SerializeField] private Button _closeButton;

        private AudioSystem _audioSystem;
        private LoseWindow _loseWindow;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseWindow);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseWindow);
        }

        public void WindowInit(LoseWindow loseWindow)
        {
            ReviewYG = GetComponent<ReviewYG>();
            _audioSystem = AudioSystem.Instance;
            _loseWindow = loseWindow;
        }

        public void CanOpened()
        {
            PlayerLoseCount++;

            float randChance = Random.value;

            if (randChance < _windowOpenChance && PlayerLoseCount >= PlayerLoseCountMax)
            {
                CanBeOpened = true;
                PlayerLoseCount = 0;
            }
            else
                CanBeOpened = false;
        }

        private void CloseWindow()
        {
            _audioSystem.PlaySoundByID(SoundID.Click);
            gameObject.SetActive(false);
            _loseWindow.Open();
        }

        protected override void OnClosed()
        {
            ReviewYG.ReviewShow();
            _loseWindow.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}