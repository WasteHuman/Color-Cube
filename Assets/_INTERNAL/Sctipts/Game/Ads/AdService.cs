using Game.Ads;
using Game.Ads.FullscreenAdSystem;
using Game.Ads.RewardedAdSystem;

namespace Sctipts.Game.Ads
{
    public static class AdService
    {
        public static IRewardedAdService RewardedAdService { get; private set; }
        public static IInterstitialAdService InterstitialAdService { get; private set; }

        public static void Initialize()
        {
#if UNITY_ANDROID
            RewardedAdService ??= new AndroidRewardedAdService();
            InterstitialAdService ??= new AndroidFullscreenAdService();

            InterstitialAdService?.SetupLoader();
            RewardedAdService?.SetupLoader();
#elif !UNITY_ANDROID
            RewardedAdService ??= new WebGLRewardedAdService();
            InterstitialAdService ??= new WebGLFullscreenAdService();

            InterstitialAdService?.SetupLoader();
            RewardedAdService?.SetupLoader();
#endif
        }
    }
}