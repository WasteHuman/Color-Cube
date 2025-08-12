using System;
using UI.States;
using UnityEngine;

namespace Game.Ads
{
    public class PlayerDataHolder : MonoBehaviour
    {
        [field: SerializeField] public PlayerDataTemplateForRewardedAd PlayerDataTemplate {  get; private set; }

        [Space(10), Header("Play state")]
        [SerializeField] private PlayState _playState;

        public event Action<int, int, float> OnAdActivated;

        private void OnEnable()
        {
            _playState.OnPlayerLose += HandlePlayerLose;
        }

        private void OnDisable()
        {
            _playState.OnPlayerLose -= HandlePlayerLose;
        }

        public void LoadPlayerData()
        {
            if (!PlayerDataTemplate.IsAdActivated)
                return;

            int coins = PlayerDataTemplate.PlayerCoins;
            int score = PlayerDataTemplate.PlayerScore;
            float factor = PlayerDataTemplate.DifficultFactor;

            OnAdActivated?.Invoke(coins, score, factor);
            PlayerDataTemplate.ToggleAdState(false);
        }

        private void HandlePlayerLose(int coins, int score, float factor)
        {
            PlayerDataTemplate.SaveCoins(coins);
            PlayerDataTemplate.SaveScore(score);
            PlayerDataTemplate.SaveDifficultFactor(factor);
        }
    }
}