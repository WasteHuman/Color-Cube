using System;
using UnityEngine;

namespace Game.GameCore
{
    public class SmoothDifficultChanger
    {
        private readonly float _minDifficult = 0.1f;
        private readonly float _maxDifficult = 1f;

        private float _currentProgress;
        private float _progressStep;
        private float _difficultFactor;

        public float DifficultFactor => _difficultFactor;

        public event Action<float> DifficultChanged;

        public SmoothDifficultChanger(float progressStep)
        {
            _progressStep = progressStep;
            _currentProgress = 0f;
        }

        public float LoadDifficultFactor(float factor)
        {
            return _difficultFactor = factor;
        }

        public void ChangeDifficult()
        {
            _currentProgress = Mathf.Clamp01(_currentProgress + _progressStep);

            _difficultFactor = Mathf.Lerp(_maxDifficult, _minDifficult, _currentProgress);

            DifficultChanged?.Invoke(_difficultFactor);
        }
    }
}