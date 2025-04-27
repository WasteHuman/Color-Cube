using System;
using UnityEngine;

namespace Gameplay.GameCore
{
    public class SmoothDifficultChanger
    {
        private readonly float _minDifficult = 0.1f;
        private readonly float _maxDifficult = 1f;

        private float _currentProgress;
        private float _progressStep;

        public event Action<float> DifficultChanged;

        public SmoothDifficultChanger(float progressStep)
        {
            _progressStep = progressStep;
            _currentProgress = 0f;
        }

        public void ChangeDifficult()
        {
            _currentProgress = Mathf.Clamp01(_currentProgress + _progressStep);

            float difficultFactor = Mathf.Lerp(_maxDifficult, _minDifficult, _currentProgress);

            DifficultChanged?.Invoke(difficultFactor);
        }
    }
}