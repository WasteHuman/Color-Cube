using System;

namespace Gameplay.Timer
{
    public interface ITimer
    {
        event Action TimeEnded;
        void Start();
        void Tick();
        void Stop();
        void ResetTime();
        bool Reset();
    }
}