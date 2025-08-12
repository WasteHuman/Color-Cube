using Core.Interfaces;

namespace Gameplay.GameplayStates
{
    public class StateMachine
    {
        private IState _currentState;

        public void SetState(IState newState)
        {
            if (_currentState != newState)
            {
                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
            }
        }

        public void Tick()
        {
            _currentState?.Tick();
        }
    }
}