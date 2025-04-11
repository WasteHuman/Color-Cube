using System;
using UnityEngine;

namespace Gameplay
{
    public class StateChecker : MonoBehaviour
    {
        [SerializeField] private VariantsHolder _variantsHolder;

        private bool _isNext = false;
        private bool _isLose = false;

        public event Action<bool> PlayerIsWin;
        public event Action<bool> PlayerIsLose;

        private void OnEnable()
        {
            _variantsHolder.PlayerClicked += CheckPlayerStatus;
        }

        private void OnDisable()
        {
            _variantsHolder.PlayerClicked -= CheckPlayerStatus;
        }

        private void Start()
        {
            _variantsHolder.InitializeHolder();
        }

        private void CheckPlayerStatus(bool isRight)
        {
            if (isRight)
            {
                _isNext = true;
                _isLose = false;

                PlayerIsWin?.Invoke(_isNext);
            }
            else
            {
                _isLose = true;
                _isNext = false;

                PlayerIsLose?.Invoke(_isLose);
            }
        }
    }
}