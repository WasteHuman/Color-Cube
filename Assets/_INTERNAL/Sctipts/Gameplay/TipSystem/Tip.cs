using Gameplay.Player;
using Gameplay.Timer;
using UnityEngine;

namespace Gameplay.TipSystem
{
    public abstract class Tip : MonoBehaviour, ITip
    {
        private TimerWithOutSlider _cooldownTimer;

        public virtual TipState State { get; protected set; }
        public virtual int Cost { get; protected set; }
        public virtual float TipCooldownTime { get; protected set; }
        public virtual float CurrentTime { get => _cooldownTimer.CurrentTime; }

        private bool CanUseTip()
        {
            return PlayerWallet.GetWallet() >= Cost && !_cooldownTimer.IsRunning;
        }

        public void InitializeTimer(float time)
        {
            TipCooldownTime = time;

            _cooldownTimer = new(TipCooldownTime);
        }

        public void StartCooldownTimer()
        {
            _cooldownTimer.Start();
        }

        public void StopCooldownTimer()
        {
            _cooldownTimer.Stop();
        }

        public void ResetTimer()
        {
            _cooldownTimer.Reset();
        }

        public void ResetCooldownTime()
        {
            _cooldownTimer.ResetTime();
        }

        public void Tick()
        {
            _cooldownTimer.Tick();
        }

        public void TipClicked()
        {
            if (CanUseTip())
            {
                PlayerWallet.Spend(Cost);
                TipEnabled();
            }
        }

        public void SetState(TipState newState)
        {
            if (State == newState)
                return;

            State = newState;
        }

        public abstract void Initialize();
        public abstract void TipEnabled();
        public abstract void ActiveCooldown();
        public abstract void StopAllTimers();

        protected TimerWithOutSlider CooldownTimer { get { return _cooldownTimer; } }
    }
}