using UnityEngine;

namespace MainMenu
{
    public class MainCubeView : MonoBehaviour
    {
        [SerializeField] private float _emissionForce;
        [SerializeField] private Renderer _renderer;

        private MainCubeColorRandomizer _colorRandomizer;
        private MaterialPropertyBlock _materialPropertyBlock;

        private void OnDisable()
        {
            _colorRandomizer.ColorGenerated -= OnColorGenerated;
        }

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            InitializeColorSystems();

            _colorRandomizer.ColorGenerated += OnColorGenerated;

            _colorRandomizer.GenerateColor();
        }

        private void InitializeColorSystems()
        {
            _materialPropertyBlock = new();
            _colorRandomizer = new();
        }

        private void OnColorGenerated(Color color)
        {
            _renderer.GetPropertyBlock(_materialPropertyBlock);

            _materialPropertyBlock.SetColor("_Color", color);
            _materialPropertyBlock.SetColor("_Emission", color * _emissionForce);

            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}