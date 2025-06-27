using Gameplay.GameplayStates;
using Gameplay.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Gameplay.Ads.RewardedAdSystem
{
    public class RewardedAd : MonoBehaviour
    {
        [Header("Player data holder")]
        [SerializeField] private PlayerDataHolder _playerDataHolder;

        [Space(10), Header("Rewarded ad UI")]
        [SerializeField] private Button _showAdButton;
        [SerializeField] private RewardedAdUI _rewardedAdUI;

        [Space(10), Header("States holder")]
        [SerializeField] private StatesHolder _statesHolder;

        [SerializeField] private SceneAsset _gameplayScene;
        [SerializeField] private bool _isDebug = false;
        
        private int _adCountMax = 3;
        private int _adCount;

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Rewarded;

            if(_isDebug)
                PlayerPrefs.DeleteKey(PlayerPrefsConsts.REWARDED_SHOWED_AD_COUNT);

            _adCount = PlayerPrefs.GetInt(PlayerPrefsConsts.REWARDED_SHOWED_AD_COUNT, _adCountMax);

            CanAdBeShowed(_showAdButton);
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Rewarded;
            _showAdButton.onClick.RemoveListener(ShowAd);
        }

        public Button GetButton(Button button)
        {
            _rewardedAdUI.UpdateCounter(_adCount, _adCountMax);
            button.onClick.AddListener(ShowAd);

            CanAdBeShowed(button);

            return _showAdButton = button;
        }

        private void Rewarded(int index = 0)
        {
            //SceneManager.LoadSceneAsync(_gameplayScene.name);
            _statesHolder.SetPlayState();
        }

        private void ShowAd()
        {
            _playerDataHolder.PlayerDataTemplate.ToggleAdState(true);

            bool flowControl = CanAdBeShowed(_showAdButton);
            if (!flowControl)
            {
                return;
            }

            YandexGame.RewVideoShow(0);

            AdShowCounter();
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