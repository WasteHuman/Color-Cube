using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class TimerHolder : MonoBehaviour
    {
        [SerializeField] private StateChecker _stateChecker;

        [SerializeField] private float _startTime;
        [SerializeField] private Slider _timerSlider;

        private Timer _timer;

        private void OnEnable()
        {
            _stateChecker.IsPlayerWin += OnPlayerWin;
            _stateChecker.IsPlayerLose += OnPlayerLose;
        }

        private void OnDisable()
        {
            _stateChecker.IsPlayerWin -= OnPlayerWin;
            _stateChecker.IsPlayerLose -= OnPlayerLose;
        }

        public void Initialize()
        {
            _timer = new(_timerSlider, _startTime);

            _timer.StartTimer();

            _timer.OnTimeEnded += OnTimeEnded;
        }

        private void Update()
        {
            _timer.Tick();
        }

        private void OnTimeEnded()
        {
            //todo проигрыш по истечению времени
        }

        private void OnPlayerWin(bool isWin)
        {
            if (!isWin)
                return;

            _timer.ResetTimer();
        }

        private void OnPlayerLose(bool isLose)
        {
            if (!isLose)
                return;

            _timer.StopTimer();
            _timer.OnTimeEnded -= OnTimeEnded;
        }
    }
}