using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

namespace Game.Ads.RewardedAdSystem
{
    public class AndroidRewardedAdService : IRewardedAdService
    {
        private RewardedAdLoader _loader;
        private YandexMobileAds.RewardedAd _rewardedAd;

        private bool _pendingShow;

        private event Action _onComplete;

        public AndroidRewardedAdService()
        {
            SetupLoader();
        }

        public void SetupLoader()
        {
            _loader = new RewardedAdLoader();
            _loader.OnAdLoaded += HandleAdLoaded;

            RequestRewarded();
        }

        public void ShowRewarded(Action onComplete = null)
        {
            _onComplete = onComplete;

            if (_rewardedAd != null)
            {
                Debug.Log("Showing rewarded ad");
                _rewardedAd.Show();
            }
            else
            {
                Debug.LogWarning("Rewarded ad not loaded yet, will show after load");
                _pendingShow = true;
                RequestRewarded();
            }
        }

        public void DestroyRewarded()
        {
            _rewardedAd?.Destroy();
            _rewardedAd = null;
        }

        private void RequestRewarded()
        {
            string adUnitId = "demo-rewarded-yandex";
            AdRequestConfiguration adRequestConfiguration = new AdRequestConfiguration.Builder(adUnitId).Build();
            _loader.LoadAd(adRequestConfiguration);
        }

        private void HandleAdLoaded(object sender, RewardedAdLoadedEventArgs e)
        {
            _rewardedAd = e.RewardedAd;
            Debug.Log($"YandexAds: ad loaded {e}");

            _rewardedAd.OnRewarded += HandleRewarded;
            _rewardedAd.OnAdShown += HandleAdShown;
            _rewardedAd.OnAdFailedToShow += HandleAdFailedToShow;
            _rewardedAd.OnAdDismissed += HandleAdDismissed;

            if (_pendingShow)
            {
                _pendingShow = false;
                ShowRewarded(_onComplete);
            }
        }

        private void HandleAdDismissed(object sender, EventArgs e)
        {
            DestroyRewarded();
            RequestRewarded();
        }

        private void HandleAdFailedToShow(object sender, AdFailureEventArgs e)
        {
            DestroyRewarded();
            RequestRewarded();
        }

        private void HandleAdShown(object sender, EventArgs e)
        {
            Debug.Log($"YandexAds: ad shown {e}");
            _onComplete?.Invoke();
            _onComplete = null;
        }

        private void HandleRewarded(object sender, Reward e)
        {
            Debug.Log($"YandexAds: rewarded {e}");
            _onComplete?.Invoke();
            _onComplete = null;
        }
    }
}