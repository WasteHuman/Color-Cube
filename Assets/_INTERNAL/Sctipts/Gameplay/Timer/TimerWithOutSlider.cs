using System;
using UnityEngine;

namespace Gameplay.Timer
{
    public class TimerWithOutSlider : ITimer
    {
        private float _currentTime;
        private float _startTime;

        private bool _isRunning;

        public float CurrentTime => _currentTime;
        public bool IsRunning => _isRunning;

        public event Action TimeEnded;

        public TimerWithOutSlider(float time)
        {
            _startTime = time;
            _currentTime = _startTime;

            _isRunning = false;
        }

        public void Start()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Tick()
        {
            if (_isRunning)
            {
                _currentTime -= Time.deltaTime;

                if(_currentTime <= 0f)
                {
                    _currentTime = 0f;

                    Stop();

                    TimeEnded?.Invoke();
                }
            }
        }

        public bool Reset()
        {
            _currentTime = _startTime;
            return _isRunning = true;
        }

        public void ResetTime()
        {
            _currentTime = _startTime;
        }
    }
}
