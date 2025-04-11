namespace Gameplay.GameplayStates
{
    public interface IState
    {
        void Enter(); //Entering in state
        void Tick(); //Updating in real-time
        void Exit(); //Exiting the state
    }
}