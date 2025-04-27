using System;
using UI;
using UnityEngine;

namespace Gameplay.Timer
{
    public class Timer
    {
        private SimpleSlider _timerSlider;

        private float _startTime;

        private float _time;
        private bool _isRunning;

        public float CurrentTime => _time;

        public event Action OnTimeEnded;

        public Timer(SimpleSlider slider, float startTime)
        {
            _timerSlider = slider;

            _startTime = startTime;

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
                    _isRunning = false;
                    OnTimeEnded?.Invoke();
                }
            }
        }

        public void StartTimer()
        {
            _isRunning = true;
            _timerSlider.SetDefaultAmount();
            _time = _startTime;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        public bool ResetTimer()
        {
            _isRunning = true;
            _time = _startTime;
            _timerSlider.SetDefaultAmount();

            return _isRunning;
        }
    }
}