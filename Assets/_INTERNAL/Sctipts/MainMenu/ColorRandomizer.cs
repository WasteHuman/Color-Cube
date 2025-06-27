using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MainMenu
{
    public class ColorRandomizer
    {
        private float _lerpDuration;
        private float _lerpSpeed;

        private Color _currentColor;
        private Color _nextColor;

        public event Action<bool> ColorGenerated;

        public ColorRandomizer(float lerpSpeed)
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

        public void GenerateColor(float minS, float maxS, float minB, float maxB)
        {
            float randHue = Random.value;
            float randSaturation = Random.Range(minS, maxS);
            float randBrightness = Random.Range(minB, maxB);

            _nextColor = Color.HSVToRGB(randHue, randSaturation, randBrightness);

            ColorGenerated?.Invoke(true);
        }

        public Color FirstColor()
        {
            float randHue = Random.value;
            float randSaturation = Random.Range(0.6f, 1f);
            float randBrightness = Random.Range(0.8f, 1f);

            Color newColor = Color.HSVToRGB(randHue, randSaturation, randBrightness);

            _currentColor = newColor;

            ColorGenerated?.Invoke(true);
            return newColor;
        }
    }
}