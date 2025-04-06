using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class Timer
    {
        private Slider _timerSlider;

        private float _startTime;

        private float _time;
        private bool _isRunning;

        public event Action OnTimeEnded;

        public Timer(Slider slider, float startTime)
        {
            _timerSlider = slider;

            _startTime = startTime;

            _timerSlider.maxValue = startTime;
            _timerSlider.minValue = 0f;
        }

        public void Tick()
        {
            if(_isRunning)
            {
                _time -= Time.deltaTime;
                _timerSlider.value = _time;

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
            _timerSlider.value = _startTime;
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
            _timerSlider.value = _time;

            return _isRunning;
        }
    }
}