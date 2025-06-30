using Gameplay.Timer;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Gameplay.TipSystem.ConcreteTips.AddTime
{
    public class AddTime : Tip
    {
        [Header("Tip settings")]
        [SerializeField] private int _minCost;
        [SerializeField] private int _cost;
        [SerializeField] private float _addTimeBonus;
        [SerializeField] private float _cooldownTime;

        [Space(10), Header("Refs")]
        [SerializeField] private TimerHolder _timerHolder;
        [SerializeField] private Image _cooldownImage;

        private Button _button;
        private TipUIController _ui;

        public override int MinimumCost
        {
            get
            {
                return _minCost;
            }
        }

        public override int Cost
        {
            get
            {
                return _cost;
            }
            protected set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Cost), "Cost cannot be negative");

                _cost = value;
            }
        }
        public override float TipCooldownTime
        {
            get
            {
                return _cooldownTime;
            }
            protected set
            {
                if (value < 0f)
                    throw new ArgumentOutOfRangeException(nameof(TipCooldownTime), "Cooldown time cannot be negative");

                _cooldownTime = value;
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(TipClicked);
            CooldownTimer.TimeEnded -= OnCooldownEnded;
        }

        private void Update()
        {
            switch (State)
            {
                case TipState.Active:
                    break;
                case TipState.Cooldown:
                    UpdateCooldown();
                    break;
                case TipState.Inactive:
                    SetDefaultState();
                    break;
            }
        }

        public override void Initialize()
        {
            ButtonInitialize();
            InitializeTimer(_cooldownTime);

            _ui = new(_cooldownImage, _button);

            CooldownTimer.TimeEnded += OnCooldownEnded;

            SetDefaultState();
            SetState(TipState.Inactive);
        }

        public override void ActiveCooldown()
        {
            SetState(TipState.Cooldown);

            SetButtonState(false);
            _ui.ShowCooldown(true);
            StartCooldownTimer();
        }

        public override void TipEnabled()
        {
            if (CanTipActivated())
            {
                SetState(TipState.Active);
                _timerHolder.AddTime(_addTimeBonus);
                ActiveCooldown();
            }
        }

        public override void StopAllTimers()
        {
            CooldownTimer.Stop();
        }

        private void SetDefaultState()
        {
            SetState(TipState.Inactive);
            StopCooldownTimer();
            ResetCooldownTime();
            SetButtonState(true);
            _ui.ShowCooldown(false);
        }

        private void SetButtonState(bool value)
        {
            _ui.SetButtonInteracteble(value);
        }

        private void UpdateCooldown()
        {
            Tick();
            _ui.SetCooldownProgress(CurrentTime / TipCooldownTime);
        }

        private void ButtonInitialize()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(TipClicked);
        }

        private bool CanTipActivated()
        {
            if (_timerHolder.CurrentTime < _timerHolder.StartTime - _addTimeBonus)
            {
                return true;
            }

            return false;
        }

        private void OnCooldownEnded()
        {
            SetDefaultState();
        }
    }
}