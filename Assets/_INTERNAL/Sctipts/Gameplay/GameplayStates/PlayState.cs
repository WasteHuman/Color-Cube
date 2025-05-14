using Gameplay.GameCore;
using Gameplay.Player;
using Gameplay.Score;
using Gameplay.Timer;
using Gameplay.TipSystem;
using System;
using UI;
using UnityEngine;

namespace Gameplay.GameplayStates
{
    public class PlayState : MonoBehaviour, IState
    {
        [Header("Game systems")]
        [SerializeField] private TimerHolder _timerHolder;
        [SerializeField] private StateChecker _stateChecker;
        [SerializeField] private VariantsHolder _variantsHolder;
        [SerializeField] private TipsHolder _tipsHolder;
        [SerializeField] private CoinsTextPool _coinsTextPool;

        [Space(10), Header("States additions")]
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private WalletView _walletView;

        [Space(10), Header("Difficult settings")]
        [SerializeField] private float _progressStep;

        private SmoothDifficultChanger _smoothDifficultChanger;

        public SmoothDifficultChanger SmoothDifficultChanger => _smoothDifficultChanger;

        public event Action<bool> TimesUp;
        public event Action OnPlayerWin;

        private void OnTimeEnded()
        {
            TimesUp?.Invoke(true);
        }

        public void Enter()
        {
            PlayStateInitialize();
            GameSystemsInitialize();
        }

        public void Tick()
        {
            _timerHolder.Tick();
        }

        public void Exit()
        {
            _timerHolder.TimeEnded -= OnTimeEnded;

            _timerHolder.StopTimer();

            _variantsHolder.Unsubscribe();
            _stateChecker.Unsubscribe();
        }

        public void Next()
        {
            _scoreView.ScoreCounter?.Add();

            PlayerWallet.Add();

            _timerHolder.ResetTimer();
            DifficultUp();

            OnPlayerWin?.Invoke();
        }

        private void PlayStateInitialize()
        {
            _timerHolder.TimeEnded += OnTimeEnded;

            _timerHolder.Initialize();

            _smoothDifficultChanger = new(_progressStep);

            _walletView.InitializeWallet(PlayerWallet.GetWallet());
            _walletView.InitializePool(_coinsTextPool);
        }

        private void GameSystemsInitialize()
        {
            _variantsHolder.InitializeHolder(_stateChecker);
            _variantsHolder.Subscribe();

            _stateChecker.InitializeChecker(_variantsHolder);
            _stateChecker.Subscribe();

            _tipsHolder.InitializeHolder();

            _coinsTextPool.Initialization();
        }

        private void DifficultUp()
        {
            _smoothDifficultChanger.ChangeDifficult();
        }
    }
}