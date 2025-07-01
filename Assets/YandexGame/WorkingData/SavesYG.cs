
using Gameplay.SaveAndLoadServices;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны

        public int PlayerWallet;
        public int BestPlayerScore;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
        }

        public static SavesYG operator+(SavesYG savesYG, SaveData saveData)
        {
            savesYG.PlayerWallet = saveData.PlayerWallet;
            savesYG.BestPlayerScore = saveData.BestScore;

            return savesYG;
        }
    }
}
