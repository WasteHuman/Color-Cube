using System;
using UnityEngine;

namespace Gameplay.Player
{
    public static class PlayerWallet
    {
        private static int _wallet;
        private static int _increaseRate = 1;

        public static int Wallet
        {
            get {  return _wallet; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Wallet), "Wallet cannot be negative");

                if(_wallet != value)
                    _wallet = value;
            }
        }
        public static int IncreaseRate
        {
            get
            {
                return _increaseRate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(IncreaseRate),"Increase Rate cannot be negative");

                _increaseRate = value;
            }
        }

        public static event Action<int, int> WalletChanged;

        public static void Add()
        {
            Wallet += IncreaseRate;

            WalletChanged?.Invoke(Wallet, IncreaseRate);
        }

        public static void Spend(int value)
        {
            if(value < 0)
                return;

            Wallet -= value;

            WalletChanged?.Invoke(Wallet, -value);
        }

        public static void SaveWallet()
        {
            PlayerPrefs.SetInt(PlayerPrefsConsts.WALLET, Wallet);
            PlayerPrefs.Save();
        }

        public static void LoadWallet()
        {
            Wallet = PlayerPrefs.GetInt(PlayerPrefsConsts.WALLET);
        }

        public static int GetWallet()
        {
            return Wallet;
        }
    }
}