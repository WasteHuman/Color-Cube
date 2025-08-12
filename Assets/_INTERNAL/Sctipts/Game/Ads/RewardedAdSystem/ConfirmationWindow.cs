using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ads.RewardedAdSystem
{
    public class ConfirmationWindow : SimpleWindow
    {
        [SerializeField] private Button _confirmationButton;
        [SerializeField] private Button _denyButton;

        private PlayerDataHolder _playerDataHolder;

        public event Action OnConfirmed;

        private void OnEnable()
        {
            _confirmationButton.onClick.AddListener(ShowAd);
            _denyButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _confirmationButton.onClick.RemoveListener(ShowAd);
            _denyButton.onClick.RemoveListener(Close);
        }

        public PlayerDataHolder GetDataHolder(PlayerDataHolder holder)
        {
            return _playerDataHolder = holder;
        }

        private void ShowAd()
        {
            _playerDataHolder.PlayerDataTemplate.ToggleAdState(true);

            OnConfirmed?.Invoke();
            Close();
        }
    }
}