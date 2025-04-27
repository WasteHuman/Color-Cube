using TMPro;
using UnityEngine;

namespace Gameplay.Player
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _walletText;

        public void UpdatePlayerWalletText(int value)
        {
            _walletText.text = $"{value}";
        }
    }
}