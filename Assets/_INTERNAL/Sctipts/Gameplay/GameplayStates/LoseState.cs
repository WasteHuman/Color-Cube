using GameWindows;
using UnityEngine;

namespace Gameplay.GameplayStates
{
    public class LoseState : MonoBehaviour, IState
    {
        [SerializeField] private LoseWindow _loseWindowPrefab;
        [SerializeField] private Transform _canvasTransform;

        public void Enter()
        {
            Instantiate(_loseWindowPrefab, _canvasTransform);
        }

        public void Tick() { }

        public void Exit()
        {

        }
    }
}