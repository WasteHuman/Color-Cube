using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.GameCore
{
    public class ColorsRandomizer : MonoBehaviour
    {
        [SerializeField] private VariantsHolder _variantsHolder;

        //[Space(10), Header("Color Minimum Offset")]
        //[SerializeField] private float _rMinOffset;
        //[SerializeField] private float _gMinOffset;
        //[SerializeField] private float _bMinOffset;

        //[Space(10), Header("Color Maximum Offset")]
        //[SerializeField] private float _rMaxOffset;
        //[SerializeField] private float _gMaxOffset;
        //[SerializeField] private float _bMaxOffset;

        [Space(10), Header("Hue Offset Settings (Degrees)")]
        [SerializeField] private float _minHueOffest = 5f;
        [SerializeField] private float _maxHueOffset = 30f;

        private Color _currentColor;
        private float _difficultFactor = 0f;

        public event Action<Color> MainColorGenerated;

        public void SubscribeOnEvents()
        {
            _variantsHolder.RightChosen += RandomizeMainVariantColor;
            _variantsHolder.MainColorSetted += RandomizeVariantsColor;
        }

        public void UnsubscribeFromEvents()
        {
            _variantsHolder.RightChosen -= RandomizeMainVariantColor;
        }

        private void RandomizeMainVariantColor()
        {
            float randHue = Random.value;

            float saturation = Random.Range(0.6f, 1f);
            float value = Random.Range(0.8f, 1f);

            _currentColor = Color.HSVToRGB(randHue, saturation, value);

            MainColorGenerated?.Invoke(_currentColor);
        }

        private void RandomizeVariantsColor(List<Variant> varints)
        {
            foreach (Variant variant in varints)
            {
                variant.SetNewMaterialColor(GenerateNewColorWithOffset(_currentColor));
                variant.SetOffRight();
            }
        }

        private Color GenerateNewColorWithOffset(Color mainColor)
        {
            Color.RGBToHSV(mainColor, out float hue, out float saturation, out float value);

            float hueOffestDeg = Mathf.Lerp(_maxHueOffset, _minHueOffest, _difficultFactor);
            float newHue = hue + Random.Range(-hueOffestDeg, hueOffestDeg) / 360f;
            newHue = (newHue + 1f) % 1f;

            return Color.HSVToRGB(newHue, saturation, value);
        }

        public void SetDifficultFactor(float factor)
        {
            _difficultFactor = Mathf.Clamp01(factor);
            Debug.Log($"Difficult factor: {_difficultFactor}");
        }
    }
}