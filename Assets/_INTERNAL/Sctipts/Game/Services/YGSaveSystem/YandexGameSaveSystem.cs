using Game.Ads.Data;
using YG;

namespace Gameplay.SaveAndLoadServices.YGSaveSystem
{
    public class YandexGameSaveSystem : ISaveSystem, ILoadSystem
    {
        public void Save(SaveData saveData)
        {
            YG2.saves += saveData;
            YG2.SaveProgress();
        }

        public SaveData Load()
        {
            return (SaveData)YG2.saves;
        }
    }
}
