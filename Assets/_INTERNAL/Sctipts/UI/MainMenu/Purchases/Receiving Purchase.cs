using Game.GameCore;
using UI.GameWindows;
using UnityEngine;
using YG;

namespace UI.MainMenu.Purchases
{
    public class ReceivingPurchase : MonoBehaviour
    {
        [SerializeField] private FailedPurchaseWindow _failedWindowPrefab;
        
        private FailedPurchaseWindow _failedWindow;

        private void OnEnable()
        {
            YG2.onPurchaseSuccess += SuccessPurchase;
            YG2.onPurchaseFailed += FailedPurchase;
        }

        private void OnDisable()
        {
            YG2.onPurchaseSuccess -= SuccessPurchase;
            YG2.onPurchaseFailed -= FailedPurchase;
        }

        private void SuccessPurchase(string id)
        {
            string coinsKey = "Wallet";
            int coins = YG2.GetState(coinsKey);

            switch (id)
            {
                case "coins_20":
                    YG2.SetState(coinsKey, coins + 20);
                    PlayerWallet.LoadWallet(YG2.GetState(coinsKey));
                    break;
                case "coins_50":
                    YG2.SetState(coinsKey, coins + 50);
                    PlayerWallet.LoadWallet(YG2.GetState(coinsKey));
                    break;
                case "coins_100":
                    YG2.SetState(coinsKey, coins + 100);
                    PlayerWallet.LoadWallet(YG2.GetState(coinsKey));
                    break;
            }
        }

        private void FailedPurchase(string id)
        {
            _failedWindow = _failedWindow != null ? _failedWindow : Instantiate(_failedWindowPrefab, transform);

            _failedWindow.Open();
        }
    }
}