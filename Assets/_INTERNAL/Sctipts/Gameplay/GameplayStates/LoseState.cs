using Gameplay.Ads.FullscreenAdSystem;
using Gameplay.Ads.RewardedAdSystem;
using Gameplay.Player;
using Gameplay.Review;
using Gameplay.Score;
using GameWindows;
using System;
using UnityEngine;
using YG;

namespace Gameplay.GameplayStates
{
    public class LoseState : MonoBehaviour, IState
    {
        [Header("Lose state UI")]
        [SerializeField] private LoseWindow _loseWindowPrefab;
        [SerializeField] private Transform _canvasTransform;

        [Space(10), Header("States Additions")]
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private RewardedAd _rewardAd;
        [SerializeField] private RewardedAdUI _rewardedAdUI;
        [SerializeField] private ReviewWindow _reviewWindowPrefab;

        [Space(10), Header("Fullscreen ad system")]
        [SerializeField] private FullscreenAdMono _adMono;

        private LoseWindow _loseWindow;
        private ReviewWindow _reviewWindow;

        public event Action OnGameRestared;

        public void Enter()
        {
            LoseWindowInit();
            InitReviewWindow();

            TryOpenReviewWindow();
            ChoiceWindow();

            _adMono.InitializeFullscreenAdMono();

            _loseWindow.OnGameRestared += HanldeGameRestarted;
            _loseWindow.DisplayCurrentScore(_scoreView.ScoreCounter.Score);
            _loseWindow.AudioSystem.PlaySoundByID(SoundsSystem.SoundID.Lose);

            _scoreView.ScoreCounter?.RecordNewBest();

            PlayerWallet.SaveWallet();

            YandexGame.GameplayStop();
            _adMono.Show();

            Time.timeScale = 0f;
        }

        public void Tick() { }

        public void Exit()
        {
            YandexGame.GameplayStart();

            _loseWindow.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        private void LoseWindowInit()
        {
            if (_loseWindow == null)
            {
                _loseWindow = Instantiate(_loseWindowPrefab, _canvasTransform);
                _rewardedAdUI.GetAdCounterText(_loseWindow.ShowedAdCounter);
                _rewardAd.GetButton(_loseWindow.ShowRewardedAdButton);
            }
        }

        private void InitReviewWindow()
        {
            if (_reviewWindow == null)
                _reviewWindow = Instantiate(_reviewWindowPrefab, _canvasTransform);

            _reviewWindow.WindowInit(_loseWindow);
        }

        private void ChoiceWindow()
        {
            if (_reviewWindow.CanBeOpened)
                _reviewWindow.Open();
            else
                _loseWindow.Open();
        }

        private void TryOpenReviewWindow()
        {
            _reviewWindow.CanOpened();
        }

        private void HanldeGameRestarted()
        {
            OnGameRestared?.Invoke();
        }
    }
}