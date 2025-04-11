using UnityEngine;

namespace Gameplay.GameplayStates
{
    public class StatesHolder : MonoBehaviour
    {
        [Header("States")]
        [SerializeField] private PlayState _playState;
        [SerializeField] private LoseState _loseState;

        [Space(10), Header("State checker")]
        [SerializeField] private StateChecker _stateChecker;

        private StateMachine _stateMachine;

        private void OnEnable()
        {
            _stateChecker.PlayerIsWin += OnPlayerWin;
            _stateChecker.PlayerIsLose += OnPlayerLose;

            _playState.TimesUp += OnPlayerLose;
        }

        private void OnDisable()
        {
            _stateChecker.PlayerIsWin -= OnPlayerWin;
            _stateChecker.PlayerIsLose -= OnPlayerLose;

            _playState.TimesUp -= OnPlayerLose;
        }

        private void Start()
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

            _playState.ResetTimer();
        }

        private void OnPlayerLose(bool isLose)
        {
            _stateMachine.SetState(_loseState);
        }
    }
}