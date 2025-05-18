using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

namespace MainMenu
{
    public class MainCubeView : MonoBehaviour
    {
        [SerializeField] private float _emissionForce;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _currentColor;

        [Space(10), Header("Color generation")]
        [SerializeField] private float _colorLerpSpeed;
        [SerializeField] private Button _generateNewColorButton;

        private MainCubeColorRandomizer _colorRandomizer;
        private MaterialPropertyBlock _materialPropertyBlock;

        private bool _isColorGenerated = false;

        private void OnDisable()
        {
            _colorRandomizer.ColorGenerated -= OnColorGenerated;
            _generateNewColorButton.onClick.RemoveListener(_colorRandomizer.GenerateColor);
        }

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            InitializeColorSystems();

            _generateNewColorButton.onClick.AddListener(_colorRandomizer.GenerateColor);
            _colorRandomizer.ColorGenerated += OnColorGenerated;

            _colorRandomizer.GenerateColor();
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

        private void LerpColor()
        {
            Color currentColor = _colorRandomizer.LerpColor();

            _renderer.GetPropertyBlock(_materialPropertyBlock);

            _materialPropertyBlock.SetColor("_Color", currentColor);
            _materialPropertyBlock.SetColor("_Emission", currentColor * _emissionForce);

            _renderer.SetPropertyBlock(_materialPropertyBlock);

            _currentColor = currentColor;
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