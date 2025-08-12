using Core.Data;
using Core.Interfaces;
using Game.Ads;
using Game.GameCore;
using System;
using UnityEngine;
using YG;

namespace UI.States
{
    public class PlayState : MonoBehaviour, IState
    {
        [Header("Game systems initializer")]
        [SerializeField] private PlayStateInitializer _initializer;

        [Space(10), Header("Difficult settings")]
        [SerializeField] private float _progressStep;

        [Space(10), Header("Rewarded ad system")]
        [SerializeField] private PlayerDataHolder _playerDataHolder;

        public SmoothDifficultChanger SmoothDifficultChanger => _initializer.SmoothDifficultChanger;

        public event Action<bool> TimesUp;
        public event Action OnPlayerWin;
        public event Action<int, int, float> OnPlayerLose;

        private void OnTimeEnded()
        {
            TimesUp?.Invoke(true);
        }

        public void Enter()
        {
#if !UNITY_ANDROID
            YG2.GameplayStart();
#endif
            _initializer.PlayStateInitialize(_progressStep);
            _initializer.GameSystemsInitialize();

            _initializer.TimerHolder.TimeEnded += OnTimeEnded;

            _playerDataHolder.OnAdActivated += OnAdShowed;
            _playerDataHolder.LoadPlayerData();
        }

        public void Tick()
        {
            _initializer.TimerHolder.Tick();
        }

        public void Exit()
        {
            _initializer.TimerHolder.TimeEnded -= OnTimeEnded;
            _playerDataHolder.OnAdActivated -= OnAdShowed;

            _initializer.TimerHolder.StopTimer();

            _initializer.VariantsHolder.Unsubscribe();
            _initializer.StateChecker.Unsubscribe();

            InvokePlayerLoseEvent();
#if !UNITY_ANDROID
            YG2.GameplayStop();
#endif
        }

        public void Next()
        {
            _initializer.ScoreView.ScoreCounter?.Add();

            PlayerWallet.Add();

            _initializer.TimerHolder.ResetTimer();
            DifficultUp();

            OnPlayerWin?.Invoke();
        }

        private void InvokePlayerLoseEvent()
        {
            PlayerWallet.SaveWallet();
            int coins = PlayerPrefs.GetInt(PlayerPrefsConsts.WALLET);
            int score = _initializer.ScoreView.ScoreCounter.Score;
            float factor = _initializer.SmoothDifficultChanger.DifficultFactor;

            OnPlayerLose?.Invoke(coins, score, factor);
        }

        private void OnAdShowed(int coins, int score, float factor)
        {
            _initializer.WalletView.InitializeWallet(coins);
            _initializer.ScoreView.UpdateScoreText(score);
            _initializer.SmoothDifficultChanger.LoadDifficultFactor(factor);
        }

        private void DifficultUp()
        {
            _initializer.SmoothDifficultChanger.ChangeDifficult();
        }
    }
}