using Gameplay.GameCore;
using Gameplay.Player;
using Gameplay.Score;
using Gameplay.Timer;
using System;
using UnityEngine;

namespace Gameplay.GameplayStates
{
    public class PlayState : MonoBehaviour, IState
    {
        [Header("Game systems")]
        [SerializeField] private TimerHolder _timerHolder;
        [SerializeField] private StateChecker _stateChecker;
        [SerializeField] private VariantsHolder _variantsHolder;

        [Space(10), Header("States additions")]
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private WalletView _walletView;

        [Space(10), Header("Difficult settings")]
        [SerializeField] private float _progressStep;

        private SmoothDifficultChanger _smoothDifficultChanger;

        public SmoothDifficultChanger SmoothDifficultChanger => _smoothDifficultChanger;

        public event Action<bool> TimesUp;

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
            _walletView.UpdatePlayerWalletText(PlayerWallet.GetWallet());

            _timerHolder.ResetTimer();
            DifficultUp();
        }

        private void PlayStateInitialize()
        {
            _timerHolder.TimeEnded += OnTimeEnded;

            _timerHolder.Initialize();

            _smoothDifficultChanger = new(_progressStep);

            _walletView.UpdatePlayerWalletText(PlayerWallet.GetWallet());
        }

        private void GameSystemsInitialize()
        {
            _variantsHolder.InitializeHolder(_stateChecker);
            _variantsHolder.Subscribe();

            _stateChecker.Initialize(_variantsHolder);
            _stateChecker.Subscribe();
        }

        private void DifficultUp()
        {
            _smoothDifficultChanger.ChangeDifficult();
        }
    }
}