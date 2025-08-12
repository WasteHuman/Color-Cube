using Game.GameCore;
using Gameplay.GameplayStates;
using UnityEngine;

namespace UI.States
{
    public class StatesHolder : MonoBehaviour
    {
        [Header("States")]
        [SerializeField] private PlayState _playState;
        [SerializeField] private LoseState _loseState;

        [Space(10), Header("State checker")]
        [SerializeField] private StateChecker _stateChecker;

        private StateMachine _stateMachine;

        public PlayState PlayState => _playState;

        private void OnEnable()
        {
            _stateChecker.PlayerIsWin += OnPlayerWin;
            _stateChecker.PlayerIsLose += OnPlayerLose;

            _loseState.OnGameRestared += HaldeRestartGame;
            _playState.TimesUp += OnPlayerLose;
        }

        private void OnDisable()
        {
            _stateChecker.PlayerIsWin -= OnPlayerWin;
            _stateChecker.PlayerIsLose -= OnPlayerLose;

            _loseState.OnGameRestared -= HaldeRestartGame;
            _playState.TimesUp -= OnPlayerLose;
        }

        private void Awake()
        {
            _stateMachine = new StateMachine();

            _stateMachine.SetState(_playState);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void OnPlayerWin(bool isWin)
        {
            if (!isWin)
                return;

            _playState.Next();
        }

        private void OnPlayerLose(bool isLose)
        {
            _stateMachine.SetState(_loseState);
        }

        private void HaldeRestartGame()
        {
            SetPlayState();
        }

        public void SetPlayState()
        {
            _stateMachine?.SetState(_playState);
        }

        public void SetLoseState()
        {
            _stateMachine?.SetState(_loseState);
        }
    }
}