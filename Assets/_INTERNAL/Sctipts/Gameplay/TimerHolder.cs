using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class TimerHolder : MonoBehaviour
    {
        [Header("Timer settings")]
        [SerializeField] private float _startTime;
        [SerializeField] private Slider _timerSlider;

        [Space(10), Header("Timer visual settigs")]
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;
        [SerializeField] private Image _fillArea;

        private Timer _timer;

        public event Action TimeEnded;

        private void TimerVizualization()
        {
            float time = Mathf.Clamp01(_timer.CurrentTime / _startTime);
            _fillArea.color = Color.Lerp(_endColor, _startColor, time);
        }

        public void Initialize()
        {
            _timer = new(_timerSlider, _startTime);

            _timer.StartTimer();
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
            _timer.ResetTimer();
        }

        public void StopTimer()
        {
            _timer.StopTimer();
        }
    }
}