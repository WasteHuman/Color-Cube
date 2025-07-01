using YG;

namespace Gameplay.SaveAndLoadServices.YGSaveSystem
{
    public class YandexGameSaveSystem : ISaveSystem, ILoadSystem
    {
        public void Save(SaveData saveData)
        {
            YandexGame.savesData += saveData;
            YandexGame.SaveProgress();
        }

        public SaveData Load()
        {
            return (SaveData)YandexGame.savesData;
        }
    }
}
