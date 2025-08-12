using System;

namespace Game.Ads
{
    public interface IRewardedAdService
    {
        void SetupLoader();
        void ShowRewarded(Action onComplete = null);
    }
}