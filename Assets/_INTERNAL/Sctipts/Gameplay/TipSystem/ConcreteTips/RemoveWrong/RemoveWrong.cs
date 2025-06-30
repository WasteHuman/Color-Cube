using Gameplay.GameCore;
using Gameplay.SoundsSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.TipSystem.ConcreteTips.RemoveWrong
{
    public class RemoveWrong : Tip
    {
        [Header("Tip settings")]
        [SerializeField] private int _minCost;
        [SerializeField] private int _cost;
        [SerializeField] private float _cooldownTime;
        [SerializeField] private Color _removeColor;

        [Space(10), Header("Refs")]
        [SerializeField] private VariantsHolder _variantHolder;
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
                if(value < 0)
                    throw new System.ArgumentOutOfRangeException(nameof(Cost), "Cost cannot be negative");

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
                    throw new System.ArgumentOutOfRangeException(nameof(TipCooldownTime), "Tip Cooldown time cannot be negative");
            }
        }

        private void OnDisable()
        {
            CooldownTimer.TimeEnded -= OnCooldownEnded;
            _button.onClick.RemoveListener(TipClicked);
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

        public override void ActiveCooldown()
        {
            SetState(TipState.Cooldown);

            StartCooldownTimer();
            SetButtonState(false);
            _ui.ShowCooldown(true);
        }

        public override void Initialize()
        {
            ButtonInitialization();

            InitializeTimer(TipCooldownTime);

            _ui = new(_cooldownImage, _button);

            CooldownTimer.TimeEnded += OnCooldownEnded;

            SetDefaultState();
        }

        public override void TipEnabled()
        {
            ActiveCooldown();
            RemoveWrongVariant();
        }

        public override void StopAllTimers()
        {
            CooldownTimer.Stop();
        }

        private void ButtonInitialization()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(TipClicked);
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

        private void RemoveWrongVariant()
        {
            Variant selected = TryGetWrongVariant(_variantHolder.Variants);

            selected.SetNewMaterialColor(_removeColor);
        }

        private Variant TryGetWrongVariant(List<Variant> variants)
        {
            Variant selected;

            do
            {
                int index = Random.Range(0, variants.Count);
                selected = variants[index];
            }
            while (selected.IsRight);

            return selected;
        }

        private void UpdateCooldown()
        {
            Tick();

            _ui.SetCooldownProgress(CurrentTime / TipCooldownTime);
        }

        private void OnCooldownEnded()
        {
            SetDefaultState();
        }
    }
}