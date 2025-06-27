using UnityEngine;

namespace Gameplay.Ads.FullscreenAdSystem
{
    public class FullscreenAdMono : MonoBehaviour
    {
        [SerializeField] private float _adShowChance;

        private FullscreenAdService _adService;

        public void InitializeFullscreenAdMono()
        {
            _adService ??= new(_adShowChance);
        }

        public void Show()
        {
            _adService.AdShow();
        }
    }
}