using MainMenu;
using UnityEngine;
using Color = UnityEngine.Color;

namespace UI
{
    public class MainCubeView : MonoBehaviour
    {
        [SerializeField] private float _emissionForce;
        [SerializeField] private Renderer _renderer;

        [Space(10), Header("Color animation settings")]
        [SerializeField] private float _colorLerpSpeed;

        private ColorRandomizer _colorRandomizer;
        private MaterialPropertyBlock _materialPropertyBlock;

        private bool _isColorGenerated = false;

        private void OnDisable()
        {
            _colorRandomizer.ColorGenerated -= OnColorGenerated;
            _isColorGenerated = false;
        }

        public void Initialization()
        {
            _renderer = GetComponent<Renderer>();
            InitializeColorSystems();

            _colorRandomizer.ColorGenerated += OnColorGenerated;

            SetFirstColor();
        }

        private void Update()
        {
            if (_isColorGenerated)
                LerpColor();
            else
                SetNewColor();
        }

        private void InitializeColorSystems()
        {
            _materialPropertyBlock = new();
            _colorRandomizer = new(_colorLerpSpeed);
        }

        private void SetFirstColor()
        {
            _renderer.GetPropertyBlock(_materialPropertyBlock);

            _materialPropertyBlock.SetColor("_Color", _colorRandomizer.FirstColor());
            _materialPropertyBlock.SetColor("_Emission", _colorRandomizer.FirstColor() * _emissionForce);

            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }

        private void LerpColor()
        {
            Color currentColor = _colorRandomizer.LerpColor();

            _renderer.GetPropertyBlock(_materialPropertyBlock);

            _materialPropertyBlock.SetColor("_Color", currentColor);
            _materialPropertyBlock.SetColor("_Emission", currentColor * _emissionForce);

            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }

        private void SetNewColor()
        {
            _colorRandomizer.GenerateColor();
        }

        private void OnColorGenerated(bool value)
        {
            _isColorGenerated = value;
        }
    }
}