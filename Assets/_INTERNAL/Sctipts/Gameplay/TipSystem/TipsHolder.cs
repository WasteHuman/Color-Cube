using Gameplay.GameplayStates;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.TipSystem
{
    public class TipsHolder : MonoBehaviour
    {
        [SerializeField] private List<Tip> _tips;
        [SerializeField] private PlayState _playState;

        private void OnEnable()
        {
            _playState.OnPlayerWin += OnPlayerWin;
        }

        private void OnDisable()
        {
            _playState.OnPlayerWin -= OnPlayerWin;
        }

        public void InitializeHolder()
        {
            foreach (ITip tip in _tips)
            {
                tip.Initialize();
            }
        }

        public void StopAllTipTimers()
        {
            foreach(ITip tip in _tips)
            {
                tip.StopAllTimers();
            }
        }

        private void OnPlayerWin()
        {
            foreach (ITip tip in _tips)
            {
                tip.DecreaseCost();

                if(tip.State == TipState.Active)
                    tip.ActiveCooldown();
            }
        }
    }
}