using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class ColorsRandomizer : MonoBehaviour
    {
        [SerializeField] private VariantsHolder _variantsHolder;

        [Space(10), Header("Colors Offset")]
        [SerializeField, Tooltip("Минимальная погрешность от Основого Варианта для остальных")] private float _minColorOffset;
        [SerializeField, Tooltip("Максимальная погрешность от Основого Варианта для остальных")] private float _maxColorOffset;

        public event Action<Color> MainColorGenerated;

        private void OnEnable()
        {
            _variantsHolder.RightChosen += RandomizeMainVariantColor;
            _variantsHolder.MainColorSetted += RandomizeVariantsColor;
        }

        private void OnDisable()
        {
            _variantsHolder.RightChosen -= RandomizeMainVariantColor;
            _variantsHolder.MainColorSetted -= RandomizeVariantsColor;
        }

        private void RandomizeMainVariantColor(Color mainColor)
        {
            Color newColor = new(Random.value, Random.value, Random.value);

            mainColor = newColor;

            MainColorGenerated?.Invoke(mainColor);
        }

        private void RandomizeVariantsColor(Color mainColor, List<Variant> varints)
        {
            foreach (Variant variant in varints)
            {
                variant.SetNewMaterialColor(GenerateNewColorWithOffset(mainColor));
                variant.SetOffRight();
            }
        }

        private Color GenerateNewColorWithOffset(Color mainColor)
        {
            float rOffset = Random.Range(_minColorOffset, _maxColorOffset);
            float gOffset = Random.Range(_minColorOffset, _maxColorOffset);
            float bOffset = Random.Range(_minColorOffset, _maxColorOffset);

            return new Color(
                Mathf.Clamp01(mainColor.r + rOffset),
                Mathf.Clamp01(mainColor.g + gOffset),
                Mathf.Clamp01(mainColor.b + bOffset));
        }
    }
}