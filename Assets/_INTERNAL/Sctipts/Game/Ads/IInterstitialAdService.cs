namespace Game.Ads
{
    public interface IInterstitialAdService
    {
        void SetupLoader();
        void ShowInterstitial();
        void DestroyInterstitial();
    }
}