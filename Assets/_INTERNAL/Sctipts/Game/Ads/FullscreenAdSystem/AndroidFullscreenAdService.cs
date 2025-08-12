using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

namespace Game.Ads.FullscreenAdSystem
{
    public class AndroidFullscreenAdService : IInterstitialAdService
    {
        private InterstitialAdLoader _loader;
        private Interstitial _interstitial;

        public AndroidFullscreenAdService()
        {
            SetupLoader();
        }

        public void SetupLoader()
        {
            _loader = new InterstitialAdLoader();
            _loader.OnAdLoaded += HandleInterstitialLoaded;
            _loader.OnAdFailedToLoad += HandleInterstitialFailedToLoad;

            RequestInterstitial();
        }

        private void RequestInterstitial()
        {
            string adUnitId = "R-M-16675967-1";
            AdRequestConfiguration adRequestConfiguration = new AdRequestConfiguration.Builder(adUnitId).Build();
            _loader.LoadAd(adRequestConfiguration);
        }

        public void ShowInterstitial()
        {
            _interstitial?.Show();
        }

        public void DestroyInterstitial()
        {
            _interstitial?.Destroy();
            _interstitial = null;
        }

        private void HandleInterstitialLoaded(object sender, InterstitialAdLoadedEventArgs e)
        {
            _interstitial = e.Interstitial;
            Debug.Log("YandexAds OnAdLoaded");

            _interstitial.OnAdClicked += HandleAdClicked;
            _interstitial.OnAdShown += HandleAdShown;
            _interstitial.OnAdFailedToShow += HandleAdFailedToShow;
            _interstitial.OnAdImpression += HandleAdImpression;
            _interstitial.OnAdDismissed += HandleAdDismissed;
        }

        private void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            Debug.Log("YandexAds OnAdFailedToLoad");
        }

        private void HandleAdDismissed(object sender, EventArgs e)
        {
            DestroyInterstitial();
            RequestInterstitial();
        }

        private void HandleAdFailedToShow(object sender, AdFailureEventArgs e)
        {
            DestroyInterstitial();
            RequestInterstitial();
        }

        private void HandleAdImpression(object sender, ImpressionData e)
        {
        }

        private void HandleAdShown(object sender, EventArgs e)
        {
        }

        private void HandleAdClicked(object sender, EventArgs e)
        {
        }
    }
}