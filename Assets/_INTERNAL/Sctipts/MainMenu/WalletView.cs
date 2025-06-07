using Gameplay.Interfaces;
using Gameplay.Player;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class WalletView : MonoBehaviour, IWalletView
    {
        [SerializeField] private TextMeshProUGUI _walletText;

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
    }
}