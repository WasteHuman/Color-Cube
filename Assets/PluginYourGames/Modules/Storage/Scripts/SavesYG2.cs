
using Game.Ads.Data;

namespace YG
{
    [System.Serializable]
    public partial class SavesYG
    {
        public int PlayerWallet;
        public int PlayerBestScore;

        public static SavesYG operator +(SavesYG savesYG, SaveData saveData)
        {
            savesYG.PlayerWallet = saveData.PlayerWallet;
            savesYG.PlayerBestScore = saveData.BestScore;

            return savesYG;
        }
    }
}
