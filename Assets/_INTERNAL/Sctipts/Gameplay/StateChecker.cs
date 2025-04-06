using System;
using UnityEngine;

namespace Gameplay
{
    public class StateChecker : MonoBehaviour
    {
        [SerializeField] private VariantsHolder _variantsHolder;
        [SerializeField] private TimerHolder _timerHolder;

        private bool _isNext = false;
        private bool _isLose = false;

        public event Action<bool> IsPlayerWin;
        public event Action<bool> IsPlayerLose;

        private void OnEnable()
        {
            _variantsHolder.OnPlayerClicked += CheckPlayerStatus;
        }

        private void OnDisable()
        {
            _variantsHolder.OnPlayerClicked -= CheckPlayerStatus;
        }

        private void Start()
        {
            _variantsHolder.InitializeHolder();
            _timerHolder.Initialize();
        }

        private void CheckPlayerStatus(bool isRight)
        {
            if (isRight)
            {
                _isNext = true;
                _isLose = false;

                IsPlayerWin?.Invoke(_isNext);
            }
            else
            {
                _isLose = true;
                _isNext = false;

                IsPlayerLose?.Invoke(_isLose);
            }
        }
    }
}