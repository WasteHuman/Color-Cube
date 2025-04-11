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

        public event Action<Color> RightChosen;
        public event Action<Color, List<Variant>> MainColorSetted;
        public event Action<bool> PlayerClicked;

        private void OnEnable()
        {
            _stateChecker.PlayerIsWin += OnPlayerChoseRight;

            _colorsRandomizer.MainColorGenerated += OnMainVariantColorGenerated;

            SubscribeOnVariantsEvent();
        }

        private void OnDisable()
        {
            _stateChecker.PlayerIsWin -= OnPlayerChoseRight;

            _colorsRandomizer.MainColorGenerated -= OnMainVariantColorGenerated;

            UnsubscribeOnVariantsEvent();
        }

        public void InitializeHolder()
        {
            InitializeVariants();
            RightChosen?.Invoke(_mainVariant.VariantRenderer.material.GetColor("_Color"));
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

            RightChosen.Invoke(_mainVariant.VariantRenderer.material.GetColor("_Color"));
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
    }
}