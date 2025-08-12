using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Timer
{
    public class TimerHolder : MonoBehaviour
    {
        [Header("TimerWithSlider settings")]
        [SerializeField] private float _startTime;
        [SerializeField] private SimpleSlider _timerSlider;

        [Space(10), Header("TimerWithSlider visual settigs")]
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;
        [SerializeField] private Image _fillArea;

        private TimerWithSlider _timer;

        public float CurrentTime => _timer.CurrentTime;
        public float StartTime => _startTime;

        public event Action TimeEnded;

        private void TimerVizualization()
        {
            float time = Mathf.Clamp01(_timer.CurrentTime / _startTime);
            _fillArea.color = Color.Lerp(_endColor, _startColor, time);
        }

        public void Initialize()
        {
            _timer = new(_timerSlider, _startTime);

            _timer.ResetTime();
            _timer.Start();
        }

        public void Tick()
        {
            _timer?.Tick();
            TimerVizualization();

            if (_timer.CurrentTime <= 0f)
            {
                TimeEnded?.Invoke();
            }
        }

        public void ResetTimer()
        {
            _timer.Reset();
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        public void AddTime(float value)
        {
            _timer.AddTime(value);
        }
    }
}