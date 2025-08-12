namespace Core.Interfaces
{
    public interface IWalletView
    {
        void InitializeWallet(int wallet);
        void OnWalletIncreased(int wallet, int value);
    }
}