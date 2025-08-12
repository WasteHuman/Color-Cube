using Core.Data;
using Sctipts.Game.Ads;
using UI.AdsUI;
using UI.States;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ads.RewardedAdSystem
{
    public class RewardedAd : MonoBehaviour
    {
        [Header("Player data holder")]
        [SerializeField] private PlayerDataHolder _playerDataHolder;

        [Space(10), Header("Rewarded ad UI")]
        [SerializeField] private Button _showAdConfirmButton;
        [SerializeField] private RewardedAdUI _rewardedAdUI;

        [Space(10), Header("States holder")]
        [SerializeField] private StatesHolder _statesHolder;

        [SerializeField] private bool _isDebug = false;

        private ConfirmationWindow _confirmationWindow;

        private readonly int _adCountMax = 3;
        private int _adCount;

        private void OnEnable()
        {
            if (_isDebug)
                PlayerPrefs.DeleteKey(PlayerPrefsConsts.REWARDED_SHOWED_AD_COUNT);

            _adCount = PlayerPrefs.GetInt(PlayerPrefsConsts.REWARDED_SHOWED_AD_COUNT, _adCountMax);

            CanAdBeShowed(_showAdConfirmButton);
            ResetAdCounter();
        }

        private void OnDisable()
        {
            if (_showAdConfirmButton != null)
                _showAdConfirmButton.onClick.RemoveListener(ShowAdConfirm);
        }

        public Button GetButton(Button button)
        {
            _rewardedAdUI.UpdateCounter(_adCount, _adCountMax);
            button.onClick.AddListener(ShowAdConfirm);

            CanAdBeShowed(button);

            return _showAdConfirmButton = button;
        }

        public ConfirmationWindow GetConfrimWindow(ConfirmationWindow window)
        {
            window.GetDataHolder(_playerDataHolder);
            return _confirmationWindow = window;
        }

        private void Rewarded()
        {
            _statesHolder.SetPlayState();
            AdShowCounter();
            _confirmationWindow.OnConfirmed -= ConfirmedShow;
        }

        private void ShowAdConfirm()
        {
            bool flowControl = CanAdBeShowed(_showAdConfirmButton);
            if (!flowControl)
            {
                return;
            }

            _confirmationWindow.Open();
            _confirmationWindow.OnConfirmed += ConfirmedShow;
        }

        private void ConfirmedShow()
        {
            AdService.RewardedAdService.ShowRewarded(() =>
            {
                Rewarded();
                Debug.Log($"Rewarded ads service: {AdService.RewardedAdService.GetType()}");
            });
        }

        private void ResetAdCounter()
        {
            if (_adCount == 0)
            {
                _adCount = _adCountMax;
            }
        }

        private bool CanAdBeShowed(Button button)
        {
            if (_adCount == 0 && button != null)
            {
                button.interactable = false;
                _adCount = _adCountMax;
                PlayerPrefs.SetInt(PlayerPrefsConsts.REWARDED_SHOWED_AD_COUNT, _adCount);
                return false;
            }

            return true;
        }

        private void AdShowCounter()
        {
            _adCount--;
            _rewardedAdUI.UpdateCounter(_adCount, _adCountMax);
            PlayerPrefs.SetInt(PlayerPrefsConsts.REWARDED_SHOWED_AD_COUNT, _adCount);
        }
    }
}