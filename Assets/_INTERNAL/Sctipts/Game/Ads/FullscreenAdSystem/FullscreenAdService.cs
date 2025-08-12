using Sctipts.Game.Ads;
using UnityEngine;

namespace Game.Ads.FullscreenAdSystem
{
    public class FullscreenAdService
    {
        private int _loseCount = 0;
        private readonly int _loseCountMax = 3;

        private readonly float _adShowChance;

        public FullscreenAdService(float adChance)
        {
            _adShowChance = adChance;
        }

        public void AdShow()
        {
            _loseCount++;

            float randomAdChance = Random.value;

            if(_loseCount >= _loseCountMax && randomAdChance < _adShowChance)
            {
                _loseCount = 0;
                AdService.InterstitialAdService.ShowInterstitial();
            }
        }
    }
}