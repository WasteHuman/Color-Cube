using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Variant : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Renderer _currentMaterial;
        [SerializeField] private float _emissionIntensity;

        private MaterialPropertyBlock _materialPropertyBlock;
        [SerializeField] private bool _isRight = false;

        public bool IsRight => _isRight;
        public Renderer VariantRenderer { get { return _currentMaterial; } }

        public event Action<bool> OnPlayerClicked;

        public void Initialize()
        {
            _currentMaterial = GetComponent<Renderer>();
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        public void SetNewMaterialColor(Color newColor)
        {
            _currentMaterial.GetPropertyBlock(_materialPropertyBlock);

            _materialPropertyBlock.SetColor("_Color", newColor);
            _materialPropertyBlock.SetColor("_Emission", newColor * _emissionIntensity);

            _currentMaterial.SetPropertyBlock(_materialPropertyBlock);
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

            Color currentColor = _materialPropertyBlock.GetColor("_Color");

            return currentColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPlayerClicked?.Invoke(_isRight);
        }
    }
}