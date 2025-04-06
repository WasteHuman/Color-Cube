using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class VariantsHolder : MonoBehaviour
    {
        [Header("Game systems")]
        [SerializeField] private StateChecker _stateChecker;
        [SerializeField] private ColorsRandomizer _colorsRandomizer;

        [Space(10), Header("Variants")]
        [SerializeField] private Variant _mainVariant;
        [SerializeField] private List<Variant> _variants;

        public List<Variant> Variants => _variants;

        public event Action<Color> OnRightChosen;
        public event Action<Color, List<Variant>> OnMainColorSetted;
        public event Action<bool> OnPlayerClicked;

        private void OnEnable()
        {
            _stateChecker.IsPlayerWin += OnPlayerChoseRight;

            _colorsRandomizer.OnMainColorGenerated += OnMainVariantColorGenerated;

            SubscribeOnVariantsEvent();
        }

        private void OnDisable()
        {
            _stateChecker.IsPlayerWin -= OnPlayerChoseRight;

            _colorsRandomizer.OnMainColorGenerated -= OnMainVariantColorGenerated;

            UnsubscribeOnVariantsEvent();
        }

        public void InitializeHolder()
        {
            InitializeVariants();
            OnRightChosen?.Invoke(_mainVariant.VariantRenderer.material.GetColor("_Color"));
        }

        private void InitializeVariants()
        {
            _mainVariant.Initialize();

            foreach (Variant variant in _variants)
            {
                variant.Initialize();
            }
        }

        private void SubscribeOnVariantsEvent()
        {
            foreach (Variant variant in _variants)
            {
                variant.OnPlayerClicked += OnPlayerChoseVariant;
            }
        }

        private void UnsubscribeOnVariantsEvent()
        {
            foreach (Variant variant in _variants)
            {
                variant.OnPlayerClicked -= OnPlayerChoseVariant;
            }
        }

        private void OnPlayerChoseRight(bool value)
        {
            if (!value)
                return;

            OnRightChosen.Invoke(_mainVariant.VariantRenderer.material.GetColor("_Color"));
        }

        private void OnPlayerChoseVariant(bool value)
        {
            OnPlayerClicked?.Invoke(value);
        }

        private void OnMainVariantColorGenerated(Color newColor)
        {
            _mainVariant.SetNewMaterialColor(newColor);

            OnMainColorSetted?.Invoke(_mainVariant.GetCurrentMaterial(), _variants);

            int randomVariantID = Random.Range(0, _variants.Count);
            Variant rightVariant = _variants[randomVariantID];

            rightVariant.SetNewMaterialColor(newColor);
            rightVariant.SetRightVariant();
        }
    }
}