using Gameplay.Player;
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
            YandexGame.GetDataEvent -= Load;
            SaveProgress();
        }

        public void SaveProgress()
        {
            SaveData saveData = new()
            {
                PlayerWallet = PlayerWallet.GetWallet(),
                BestScore = PlayerPrefs.GetInt(PlayerPrefsConsts.BEST_SCORE)
            };

            _saveSystem.Save(saveData);
        }

        public void LoadData()
        {
            _saveSystem = new();

            YandexGame.GetDataEvent += Load;

            if (YandexGame.SDKEnabled)
            {
                Load();

                if (_autoSaveCoroutine != null)
                    StopCoroutine(_autoSaveCoroutine);
                _autoSaveCoroutine = StartCoroutine(AutoSave());
            }
        }

        private void Load()
        {
            SaveData data = _saveSystem.Load();

            PlayerPrefs.SetInt(PlayerPrefsConsts.WALLET, data.PlayerWallet);
            PlayerWallet.LoadWallet();
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