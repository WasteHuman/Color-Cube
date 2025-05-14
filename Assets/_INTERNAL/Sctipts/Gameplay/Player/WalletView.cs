using Gameplay.Interfaces;
using TMPro;
using UI;
using UnityEngine;

namespace Gameplay.Player
{
    public class WalletView : MonoBehaviour, IWalletView
    {
        [Header("In-game text")]
        [SerializeField] private TextMeshProUGUI _walletText;

        private CoinsTextPool _pool;

        private void OnEnable()
        {
            PlayerWallet.WalletChanged += OnWalletIncreased;
        }

        private void OnDisable()
        {
            PlayerWallet.WalletChanged -= OnWalletIncreased;
        }

        public void InitializeWallet(int value)
        {
            _walletText.text = $"{value}";
        }

        public void InitializePool(CoinsTextPool pool)
        {
            _pool = pool;
        }

        public void OnWalletIncreased(int wallet, int value)
        {
            _walletText.text = $"{wallet}";

            CoinsAddedAnimation coin = _pool.GetCoinsText();

            switch (value)
            {
                case > 0:
                    coin.SetText($"+{value}");
                    break;
                case < 0:
                    coin.SetText($"{value}");
                    break;
                default:
                    Debug.LogWarning($"Value {value} incorrect!");
                    break;
            }
        }
    }
}