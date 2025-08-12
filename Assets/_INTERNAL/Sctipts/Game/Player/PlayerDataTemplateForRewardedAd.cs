using UnityEngine;

namespace Game.Ads
{
    [CreateAssetMenu(menuName = "Ads/Player data temple (for rewarded)", fileName = "Player Data")]
    public class PlayerDataTemplateForRewardedAd : ScriptableObject
    {
        [field: SerializeField] public int PlayerScore {  get; private set; }
        [field: SerializeField] public int PlayerCoins { get; private set; }
        [field: SerializeField] public float DifficultFactor { get; private set; }
        [field: SerializeField] public bool IsAdActivated { get; private set; }

        public int SaveScore(int score)
        {
            return PlayerScore = score;
        }

        public int SaveCoins(int amount)
        {
            return PlayerCoins = amount;
        }

        public float SaveDifficultFactor(float factor)
        {
            return DifficultFactor = factor;
        }

        public void ToggleAdState(bool value)
        {
            IsAdActivated = value;
        }

        public void ResetValues()
        {
            PlayerCoins = 0;
            PlayerScore = 0;

            DifficultFactor = 1f;
        }
    }
}