using Game.Timer;
using Gameplay.Timer;
using System;

namespace Gameplay.TipSystem.ConcreteTips.Freeze
{
    public class FreezeTimerLogic
    {
        private readonly TimerHolder _timeHolder;
        private readonly TimerWithOutSlider _freezeTimer;

        public float CurrentTime => _freezeTimer.CurrentTime;
        public bool IsRunning => _freezeTimer.IsRunning;

        public event Action OnFreezeEnded;

        public FreezeTimerLogic(TimerHolder timeHolder, float freezeDuration)
        {
            _timeHolder = timeHolder;
            _freezeTimer = new(freezeDuration);
            _freezeTimer.TimeEnded += () => OnFreezeEnded?.Invoke();
        }

        public void Activate()
        {
            _timeHolder.StopTimer();
            _freezeTimer.Start();
        }

        public void Tick()
        {
            _freezeTimer.Tick();
        }

        public void Deactivate()
        {
            _timeHolder.StartTimer();
            _freezeTimer.ResetTime();
        }
    }
}