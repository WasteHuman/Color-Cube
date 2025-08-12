using System;
using UnityEngine;
using YG;

namespace Game.Ads.RewardedAdSystem
{
    public class WebGLRewardedAdService : IRewardedAdService
    {
        private Action _pendingCallback;

        public WebGLRewardedAdService()
        {
            try
            {
                YG2.onRewardAdv += OnYGReward;
            }
            catch
            {
                Debug.LogWarning("YG2 SDK event not found. Make sure YG2 is present in WebGL build.");
            }
        }

        public void SetupLoader() { }

        public void ShowRewarded(Action onComplete = null)
        {
            _pendingCallback = onComplete;

            try
            {
                YG2.RewardedAdvShow("");
            }
            catch (Exception)
            {
                Debug.LogWarning("YG2.RewVideoShow call failed. Invoking callback immediately as fallback.");
                _pendingCallback?.Invoke();
                _pendingCallback = null;
            }
        }

        private void OnYGReward(string id)
        {
            _pendingCallback?.Invoke();
            _pendingCallback = null;
        }
    }
}