using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MainMenu
{
    public class MainCubeColorRandomizer
    {
        private float _lerpDuration;
        private float _lerpSpeed;

        private Color _currentColor;
        private Color _nextColor;

        public event Action<bool> ColorGenerated;

        public MainCubeColorRandomizer(float lerpSpeed)
        {
            _lerpSpeed = lerpSpeed;
        }

        public Color LerpColor()
        {
            _lerpDuration += Time.deltaTime;
            float duration = Mathf.Clamp01(_lerpDuration / _lerpSpeed);

            if (duration >= 1f)
            {
                _lerpDuration = 0f;
                _currentColor = _nextColor;
                ColorGenerated?.Invoke(false);
            }

            Color color = Color.Lerp(_currentColor, _nextColor, duration);

            return color;
        }

        public void GenerateColor()
        {
            float randHue = Random.value;
            float randSaturation = Random.Range(0.6f, 1f);
            float randBrightness = Random.Range(0.8f, 1f);

            _nextColor = Color.HSVToRGB(randHue, randSaturation, randBrightness);

            ColorGenerated?.Invoke(true);
        }
    }
}