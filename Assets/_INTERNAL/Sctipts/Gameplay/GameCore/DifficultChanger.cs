using System;
using Utils;

namespace Gameplay.GameCore
{
    public class DifficultChanger
    {
        private Counter _counter;
        private float _rDifficult;
        private float _gDifficult;
        private float _bDifficult;

        public event Action<float, float, float> DifficultChanged;

        public Counter Counter => _counter;

        public void SubsctibeOnCounterEvents()
        {
            _counter.CountChanged += OnCountChanged;
        }

        private void ChangeDifficult(int count)
        {
            switch (count)
            {
                case 10:
                    SetNewOffsets(0.65f, 0.65f, 0.65f);
                    break;

                case 15: SetNewOffsets(0.65f, 0.35f, 0.55f);
                    break;

                case 25: SetNewOffsets(0.4f, 0.25f, 0.35f);
                    break;

                case 35: SetNewOffsets(0.15f, 0.2f, 0.25f);
                    break;

                case 45: SetNewOffsets(0.1f, 0.15f, 0.1f);
                    break;

                case 55: SetNewOffsets(0f, 0.1f, 0f);
                    break;

                default: SetNewOffsets(0.7f, 0.7f, 0.7f);
                    break;
            }
        }

        private void SetNewOffsets(float rOffset, float gOffset, float bOffset)
        {
            _rDifficult = rOffset;
            _gDifficult = gOffset;
            _bDifficult = bOffset;

            DifficultChanged?.Invoke(_rDifficult, _gDifficult, _bDifficult);
        }

        private void OnCountChanged(int count)
        {
            ChangeDifficult(count);
        }
    }
}