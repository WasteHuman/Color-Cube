using Gameplay.Score;
using System;
using UnityEngine;

namespace Gameplay.GameCore
{
    public class StateChecker : MonoBehaviour
    {
        private VariantsHolder _variantsHolder;

        private bool _isNext = false;
        private bool _isLose = false;

        public event Action<bool> PlayerIsWin;
        public event Action<bool> PlayerIsLose;

        public void Subscribe()
        {
            _variantsHolder.PlayerClicked += CheckPlayerStatus;
        }

        public void Unsubscribe()
        {
            _variantsHolder.PlayerClicked -= CheckPlayerStatus;
        }

        public void InitializeChecker(VariantsHolder variantsHolder)
        {
            _variantsHolder = variantsHolder;
        }

        private void CheckPlayerStatus(bool isRight)
        {
            if (isRight)
            {
                Next();
            }
            else
            {
                Lose();
            }
        }

        private void Lose()
        {
            if (_isLose) return;

            _isLose = true;
            _isNext = false;

            PlayerIsLose?.Invoke(_isLose);

            _isLose = false;
        }

        private void Next()
        {
            if (_isNext) return;

            _isNext = true;
            _isLose = false;

            PlayerIsWin?.Invoke(_isNext);

            _isNext = false;
        }
    }
}