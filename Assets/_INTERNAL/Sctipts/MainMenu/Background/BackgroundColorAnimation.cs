using UnityEngine;

namespace MainMenu.Background
{
    public class BackgroundColorAnimation : MonoBehaviour
    {
        [Header("Animation settings")]
        [SerializeField] private float _colorTransSpeed;
        [SerializeField] private float _minSaturation;
        [SerializeField] private float _maxSaturation;
        [SerializeField] private float _minBrightness;
        [SerializeField] private float _maxBrightness;

        [Header("Background")]
        [SerializeField] private Camera _mainCamera;

        private ColorRandomizer _randomizer;

        private bool _isColorGenerated = false;

        private void OnDisable()
        {
            _randomizer.ColorGenerated -= OnColorGenerated;
            _isColorGenerated = false;
        }

        public void Initialization()
        {
            _randomizer = new(_colorTransSpeed);

            _randomizer.ColorGenerated += OnColorGenerated;

            _mainCamera.backgroundColor = _randomizer.FirstColor();
        }

        private void Update()
        {
            if (_isColorGenerated)
                LerpColor();
            else
                SetNewColor();
        }

        private void LerpColor()
        {
            Color newColor = _randomizer.LerpColor();

            _mainCamera.backgroundColor = newColor;
        }

        private void SetNewColor()
        {
            _randomizer.GenerateColor(_minSaturation, _maxSaturation, _minBrightness, _maxBrightness);
        }

        private void OnColorGenerated(bool value)
        {
            _isColorGenerated = value;
        }
    }
}