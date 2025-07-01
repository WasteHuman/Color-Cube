using YG;

namespace Gameplay.SaveAndLoadServices
{
    [System.Serializable]
    public class SaveData
    {
        public int PlayerWallet = 0;
        public int BestScore = 0;

        public static explicit operator SaveData(SavesYG savesYG)
        {
            return new SaveData()
            {
                PlayerWallet = savesYG.PlayerWallet,
                BestScore = savesYG.BestPlayerScore,
            };
        }
    }
}
