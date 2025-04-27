using System;

namespace Gameplay.Player
{
    public static class PlayerWallet
    {
        private static int _wallet;

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

        public static void Add()
        {
            Wallet++;
        }

        public static void Spend(int value)
        {
            if(value < 0)
                return;

            Wallet -= value;
        }

        public static int GetWallet()
        {
            return Wallet;
        }
    }
}