using System;
using UnityEngine;

namespace Gameplay.GameplayStates
{
    public class PlayState : MonoBehaviour, IState
    {
        [SerializeField] private TimerHolder _timerHolder;

        public event Action<bool> TimesUp;

        private void OnTimeEnded()
        {
            TimesUp?.Invoke(true);
        }

        public void Enter()
        {
            _timerHolder.TimeEnded += OnTimeEnded;

            _timerHolder.Initialize();
        }

        public void Tick()
        {
            _timerHolder.Tick();
        }

        public void Exit()
        {
            _timerHolder.TimeEnded -= OnTimeEnded;

            _timerHolder.StopTimer();
        }

        public void ResetTimer()
        {
            _timerHolder.ResetTimer();
        }
    }
}