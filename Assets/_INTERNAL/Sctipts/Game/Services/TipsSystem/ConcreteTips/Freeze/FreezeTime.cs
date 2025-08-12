using Game.Services.TipSystem;
using Game.Timer;
using Game.Tips.TipState;
using System;
using UI.Tips;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.TipSystem.ConcreteTips.Freeze
{
    public class FreezeTime : Tip
    {
        [Header("Tip settings")]
        [SerializeField] private int _minCost;
        [SerializeField] private int _cost;
        [SerializeField] private float _freezeTime;
        [SerializeField] private float _cooldownTime;

        [Space(10), Header("Refs")]
        [SerializeField] private TimerHolder _timerHolder;
        [SerializeField] private Image _cooldownImage;
        [SerializeField] private Image _activatedImage;

        private FreezeTimerLogic _freezeTimer;
        private TipUIController _ui;
        private Button _button;

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
                if (value < 0f)
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
            _freezeTimer.OnFreezeEnded -= OnFreezeEnded;
            CooldownTimer.TimeEnded -= OnCooldownEnded;
        }

        public override void Initialize()
        {
            InitializeTimer(TipCooldownTime);

            ButtonInitialize();

            _freezeTimer = new(_timerHolder, _freezeTime);
            _ui = new(_activatedImage, _cooldownImage, _button);

            SetDefaultState();

            EventsSubscribe();
        }

        private void Update()
        {
            switch (State)
            {
                case TipState.Active:
                    UpdateActive();
                    break;
                case TipState.Cooldown:
                    UpdateCooldown();
                    break;
                case TipState.Inactive:
                    SetDefaultState();
                    break;
            }
        }

        public override void TipEnabled()
        {
            _freezeTimer.Activate();
            SetButtonState(false);
            UpdateUIState(true);

            SetState(TipState.Active);
        }

        public override void ActiveCooldown()
        {
            _freezeTimer.Deactivate();
            SetButtonState(false);
            UpdateUIState(false);
            StartCooldownTimer();

            SetState(TipState.Cooldown);
        }

        public override void StopAllTimers()
        {
            _freezeTimer.Deactivate();
            CooldownTimer.Stop();
        }

        private void ButtonInitialize()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(TipClicked);
        }

        private void EventsSubscribe()
        {
            _freezeTimer.OnFreezeEnded += OnFreezeEnded;
            CooldownTimer.TimeEnded += OnCooldownEnded;
        }

        private void UpdateCooldown()
        {
            Tick();
            _ui.SetCooldownProgress(CurrentTime / TipCooldownTime);
        }

        private void UpdateActive()
        {
            _freezeTimer.Tick();
            _ui.SetActivatedProgress(_freezeTimer.CurrentTime / _freezeTime);
        }

        private void SetDefaultState()
        {
            SetState(TipState.Inactive);
            StopCooldownTimer();
            ResetCooldownTime();
            _ui.ShowCooldown(false);
            SetButtonState(true);
        }

        private void SetButtonState(bool value)
        {
            _ui.SetButtonInteracteble(value);
        }

        private void UpdateUIState(bool activated)
        {
            _ui.ShowActive(activated);
            _ui.ShowCooldown(!activated);
        }

        private void OnFreezeEnded()
        {
            SetState(TipState.Cooldown);
            ActiveCooldown();
        }

        private void OnCooldownEnded()
        {
            SetDefaultState();
        }
    }
}