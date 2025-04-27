using Gameplay.Score;
using GameWindows;
using UnityEngine;

namespace Gameplay.GameplayStates
{
    public class LoseState : MonoBehaviour, IState
    {
        [Header("Lose state UI")]
        [SerializeField] private LoseWindow _loseWindowPrefab;
        [SerializeField] private Transform _canvasTransform;

        [Space(10), Header("States Additions")]
        [SerializeField] private ScoreView _scoreView;

        public void Enter()
        {
            Instantiate(_loseWindowPrefab, _canvasTransform);
            _scoreView.ScoreCounter?.RecordNewBest();
        }

        public void Tick() { }

        public void Exit() { }
    }
}