using Core.Data;
using Game.Ads.Data;
using Game.GameCore;
using Gameplay.SaveAndLoadServices.YGSaveSystem;
using System.Collections;
using UnityEngine;
using YG;

namespace Gameplay.SaveAndLoadServices
{
    public class SaveSystemMono : MonoBehaviour
    {
        [Header("Save system settings")]
        [SerializeField] private float _autoSaveInterval;

        private Coroutine _autoSaveCoroutine;
        private YandexGameSaveSystem _saveSystem;

        private void OnApplicationQuit()
        {
            SaveProgress();
            YG2.onGetSDKData -= Load;
        }

        public void SaveProgress()
        {
            SaveData saveData = new()
            {
                PlayerWallet = PlayerWallet.Wallet,
                BestScore = PlayerPrefs.GetInt(PlayerPrefsConsts.BEST_SCORE)
            };

            _saveSystem.Save(saveData);
        }

        public void LoadData()
        {
            _saveSystem = new();

            YG2.onGetSDKData += Load;

            if (YG2.isSDKEnabled)
            {
                Load();

                if (_autoSaveCoroutine != null)
                    StopCoroutine(_autoSaveCoroutine);
                _autoSaveCoroutine = StartCoroutine(AutoSave());
            }
        }

        public void Load()
        {
            SaveData data = _saveSystem.Load();

            PlayerWallet.LoadWallet(data.PlayerWallet);
        }

        private IEnumerator AutoSave()
        {
            while (true)
            {
                yield return new WaitForSeconds(_autoSaveInterval);
                SaveProgress();
            }
        }
    }
}