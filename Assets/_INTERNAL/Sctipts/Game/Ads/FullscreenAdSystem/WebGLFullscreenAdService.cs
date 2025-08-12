using System;
using UnityEngine;
using YG;

namespace Game.Ads.FullscreenAdSystem
{
    public class WebGLFullscreenAdService : IInterstitialAdService
    {
        public void SetupLoader() { }

        public void ShowInterstitial()
        {
            try
            {
                YG2.InterstitialAdvShow();
            }
            catch (Exception)
            {
                Debug.LogWarning("YG2.InterstitialAdvShow failed or YG2 not present.");
            }
        }

        public void DestroyInterstitial() { }
    }
}
