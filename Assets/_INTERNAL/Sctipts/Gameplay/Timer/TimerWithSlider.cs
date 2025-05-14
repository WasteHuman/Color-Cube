using System;
using UI;
using UnityEngine;

namespace Gameplay.Timer
{
    public class TimerWithSlider : ITimer
    {
        private readonly SimpleSlider _timerSlider;

        private readonly float _startTime;

        private float _time;
        private bool _isRunning;

        public float CurrentTime => _time;

        public event Action TimeEnded;

        public TimerWithSlider(SimpleSlider slider, float startTime)
        {
            _timerSlider = slider;

            _startTime = startTime;
            _time = _startTime;

            _timerSlider.SetDefaultAmount();
        }

        public void Tick()
        {
            if(_isRunning)
            {
                _time -= Time.deltaTime;
                _timerSlider.SetValue(_time, _startTime);

                if (_time <= 0f)
                {
                    Stop();
                    TimeEnded?.Invoke();
                }
            }
        }

        public void Start()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public bool Reset()
        {
            _isRunning = true;
            _time = _startTime;
            _timerSlider.SetDefaultAmount();

            return _isRunning;
        }

        public void ResetTime()
        {
            _time = _startTime;
        }

        public void AddTime(float value)
        {
            _time += value;
            _timerSlider.SetValue(_time, _startTime);
        }
    }
}