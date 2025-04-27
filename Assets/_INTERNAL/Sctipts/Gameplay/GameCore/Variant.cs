using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.GameCore
{
    public class Variant : MonoBehaviour, IPointerClickHandler
    {
        [Header("Material settings")]
        [SerializeField] private Renderer _currentMaterial;
        [SerializeField] private float _emissionIntensity;

        private float _colorTransTime;
        private float _timer = 0f;

        private MaterialPropertyBlock _materialPropertyBlock;
        private Color _previousColor;
        private Color _currentColor;

        private bool _isRight = false;
        private bool _isTranslation = true;

        public Renderer VariantRenderer { get { return _currentMaterial; } }

        public event Action<bool> PlayerClicked;

        private void Update()
        {
            if (_isTranslation)
            {
                ColorTranslation(_currentColor);
            }
        }

        private void ColorTranslation(Color outColor)
        {
            _timer += Time.deltaTime;
            float time = Mathf.Clamp01(_timer / _colorTransTime);

            if (time >= 1f)
            {
                _isTranslation = false;
                _timer = 0f;
                return;
            }

            Color newColor = Color.Lerp(_previousColor, outColor, time);
            _currentMaterial.GetPropertyBlock(_materialPropertyBlock);

            _materialPropertyBlock.SetColor("_Color", newColor);
            _materialPropertyBlock.SetColor("_Emission", newColor * _emissionIntensity);

            _currentMaterial.SetPropertyBlock(_materialPropertyBlock);
        }

        public void Initialize(float colorTransTime)
        {
            _currentMaterial = GetComponent<Renderer>();
            _materialPropertyBlock = new MaterialPropertyBlock();

            _previousColor = GetCurrentMaterial();
            _colorTransTime = colorTransTime;
        }

        public void SetNewMaterialColor(Color newColor)
        {
            _previousColor = GetCurrentMaterial();

            _currentColor = newColor;

            _isTranslation = true;
        }

        public bool SetRightVariant()
        {
            return _isRight = true;
        }

        public bool SetOffRight()
        {
            return _isRight = false;
        }

        public Color GetCurrentMaterial()
        {
            _currentMaterial.GetPropertyBlock(_materialPropertyBlock);

            return _materialPropertyBlock.GetColor("_Color");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PlayerClicked?.Invoke(_isRight);
        }
    }
}