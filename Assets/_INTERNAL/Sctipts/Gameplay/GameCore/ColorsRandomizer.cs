using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.GameCore
{
    public class ColorsRandomizer : MonoBehaviour
    {
        [SerializeField] private VariantsHolder _variantsHolder;

        [Space(10), Header("Color Minimum Offset")]
        [SerializeField] private float _rMinOffset;
        [SerializeField] private float _gMinOffset;
        [SerializeField] private float _bMinOffset;

        [Space(10), Header("Color Maximum Offset")]
        [SerializeField] private float _rMaxOffset;
        [SerializeField] private float _gMaxOffset;
        [SerializeField] private float _bMaxOffset;

        private float _difficultFactor;

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

        private void RandomizeMainVariantColor(Color _)
        {
            Color newColor = new(Random.value, Random.value, Random.value);

            MainColorGenerated?.Invoke(newColor);
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
            float rOffset = Mathf.Lerp(_rMaxOffset, _rMinOffset, _difficultFactor);
            float gOffset = Mathf.Lerp(_gMaxOffset, _gMinOffset, _difficultFactor);
            float bOffset = Mathf.Lerp(_bMaxOffset, _bMinOffset, _difficultFactor);
            
            return new Color(
                Mathf.Clamp01(mainColor.r + Random.Range(-rOffset, rOffset)),
                Mathf.Clamp01(mainColor.g + Random.Range(-gOffset, gOffset)),
                Mathf.Clamp01(mainColor.b + Random.Range(-bOffset, bOffset))
                );
        }

        public void SetDifficultFactor(float factor)
        {
            _difficultFactor = Mathf.Clamp01(factor);
        }
    }
}