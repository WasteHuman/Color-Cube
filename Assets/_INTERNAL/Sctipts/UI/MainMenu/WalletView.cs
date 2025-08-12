using Core.Interfaces;
using Game.GameCore;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WalletView : MonoBehaviour, IWalletView
    {
        [SerializeField] private TextMeshProUGUI _walletText;

        private void OnEnable()
        {
            PlayerWallet.WalletLoaded += OnWalletLoaded;
        }

        private void OnDisable()
        {
            PlayerWallet.WalletLoaded -= OnWalletLoaded;
        }

        public void Initialization()
        {
            PlayerWallet.LoadWallet();
            InitializeWallet(PlayerWallet.GetWallet());
        }

        public void InitializeWallet(int wallet)
        {
            _walletText.text = $"{wallet}";
        }

        public void OnWalletIncreased(int wallet, int _) { }

        private void OnWalletLoaded(int value)
        {
            _walletText.text = $"{value}";
        }
    }
}