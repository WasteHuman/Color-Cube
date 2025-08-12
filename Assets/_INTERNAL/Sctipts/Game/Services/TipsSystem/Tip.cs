using Game.GameCore;
using Game.SoundsSystem;
using Game.Tips.TipState;
using Gameplay.Timer;
using TMPro;
using UnityEngine;

namespace Game.Services.TipSystem
{
    public abstract class Tip : MonoBehaviour, ITip
    {
        private TimerWithOutSlider _cooldownTimer;

        public virtual AudioSystem AudioSystem => AudioSystem.Instance;
        public virtual TipState State { get; protected set; }
        public virtual int MinimumCost { get; protected set; }
        public virtual int Cost { get; protected set; }
        public virtual float TipCooldownTime { get; protected set; }
        public virtual float CurrentTime { get => _cooldownTimer.CurrentTime; }
        [field: SerializeField] public virtual TextMeshProUGUI CostText { get; protected set; }

        private bool CanUseTip()
        {
            return PlayerWallet.GetWallet() >= Cost && !_cooldownTimer.IsRunning;
        }

        public void InitializeTimer(float time)
        {
            TipCooldownTime = time;

            _cooldownTimer = new(TipCooldownTime);

            CostText.text = $"{Cost}";
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
                IncreaseCost();
                AudioSystem.PlaySoundByID(SoundID.Click);
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

        public void IncreaseCost() => ModifyCost(5);

        public void DecreaseCost()
        {
            if (Cost == MinimumCost)
                return;

            ModifyCost(-3);
        }

        private void ModifyCost(int value)
        {
            if (Cost < MinimumCost)
            {
                Cost = MinimumCost;
                CostText.text = $"{Cost}";
                return;
            }
            else
            {
                Cost += value;
                CostText.text = $"{Cost}";
            }
        }

        public abstract void Initialize();
        public abstract void TipEnabled();
        public abstract void ActiveCooldown();
        public abstract void StopAllTimers();

        protected TimerWithOutSlider CooldownTimer { get { return _cooldownTimer; } }
    }
}