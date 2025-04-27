using Gameplay.GameplayStates;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.GameCore
{
    public class VariantsHolder : MonoBehaviour
    {
        [Header("Game systems")]
        [SerializeField] private ColorsRandomizer _colorsRandomizer;
        [SerializeField] private StatesHolder _statesHolder;

        [Space(10), Header("Variants")]
        [SerializeField] private Variant _mainVariant;
        [SerializeField] private List<Variant> _variants;

        [Space(10), Header("Color translation time")]
        [SerializeField] private float _colorTransTime;

        private StateChecker _stateChecker;

        public List<Variant> Variants => _variants;

        public event Action<Color> RightChosen;
        public event Action<Color, List<Variant>> MainColorSetted;
        public event Action<bool> PlayerClicked;

        public void Subscribe()
        {
            _stateChecker.PlayerIsWin += OnPlayerChoseRight;
            _statesHolder.PlayState.SmoothDifficultChanger.DifficultChanged += OnDifficultFactorChanged;

            SubscribeOnVariantsEvent();
        }

        public void Unsubscribe()
        {
            _stateChecker.PlayerIsWin -= OnPlayerChoseRight;
            _statesHolder.PlayState.SmoothDifficultChanger.DifficultChanged -= OnDifficultFactorChanged;

            _colorsRandomizer.MainColorGenerated -= OnMainVariantColorGenerated;
            _colorsRandomizer.UnsubscribeFromEvents();

            UnsubscribeOnVariantsEvent();
        }

        public void InitializeHolder(StateChecker stateChecker)
        {
            InitializeVariants();
            FirstColor();

            _stateChecker = stateChecker;
        }

        private void FirstColor()
        {
            _colorsRandomizer.SubscribeOnEvents();
            _colorsRandomizer.MainColorGenerated += OnMainVariantColorGenerated;

            RightChosen?.Invoke(_mainVariant.VariantRenderer.material.GetColor("_Color"));
        }

        private void InitializeVariants()
        {
            _mainVariant.Initialize(_colorTransTime);

            foreach (Variant variant in _variants)
            {
                variant.Initialize(_colorTransTime);
            }
        }

        private void SubscribeOnVariantsEvent()
        {
            foreach (Variant variant in _variants)
            {
                variant.PlayerClicked += OnPlayerChoseVariant;
            }
        }

        private void UnsubscribeOnVariantsEvent()
        {
            foreach (Variant variant in _variants)
            {
                variant.PlayerClicked -= OnPlayerChoseVariant;
            }
        }

        private void OnPlayerChoseRight(bool value)
        {
            if (!value)
                return;

            Color color = _mainVariant.VariantRenderer.material.GetColor("_Color");
            RightChosen.Invoke(color);
        }

        private void OnPlayerChoseVariant(bool value)
        {
            PlayerClicked?.Invoke(value);
        }

        private void OnMainVariantColorGenerated(Color newColor)
        {
            _mainVariant.SetNewMaterialColor(newColor);

            MainColorSetted?.Invoke(_mainVariant.GetCurrentMaterial(), _variants);

            int randomVariantID = Random.Range(0, _variants.Count);
            Variant rightVariant = _variants[randomVariantID];

            rightVariant.SetNewMaterialColor(newColor);
            rightVariant.SetRightVariant();
        }

        private void OnDifficultFactorChanged(float factor)
        {
            _colorsRandomizer.SetDifficultFactor(factor);
        }
    }
}