using Gameplay.GameCore;
using Gameplay.Player;
using Gameplay.Score;
using System;
using UnityEngine;

namespace Gameplay.GameplayStates
{
    public class PlayState : MonoBehaviour, IState
    {
        [Header("Game systems initializer")]
        [SerializeField] private PlayStateInitializer _initializer;

        [Space(10), Header("States additions")]
        [SerializeField] private ScoreView _scoreView;

        [Space(10), Header("Difficult settings")]
        [SerializeField] private float _progressStep;

        public SmoothDifficultChanger SmoothDifficultChanger => _initializer.SmoothDifficultChanger;

        public event Action<bool> TimesUp;
        public event Action OnPlayerWin;

        private void OnTimeEnded()
        {
            TimesUp?.Invoke(true);
        }

        public void Enter()
        {
            Time.timeScale = 1f;

            _initializer.PlayStateInitialize(_progressStep);
            _initializer.GameSystemsInitialize();

            _initializer.TimerHolder.TimeEnded += OnTimeEnded;
        }

        public void Tick()
        {
            _initializer.TimerHolder.Tick();
        }

        public void Exit()
        {
            _initializer.TimerHolder.TimeEnded -= OnTimeEnded;

            _initializer.TimerHolder.StopTimer();

            _initializer.VariantsHolder.Unsubscribe();
            _initializer.StateChecker.Unsubscribe();
        }

        public void Next()
        {
            _scoreView.ScoreCounter?.Add();

            PlayerWallet.Add();

            _initializer.TimerHolder.ResetTimer();
            DifficultUp();

            OnPlayerWin?.Invoke();
        }

        private void DifficultUp()
        {
            _initializer.SmoothDifficultChanger.ChangeDifficult();
        }
    }
}