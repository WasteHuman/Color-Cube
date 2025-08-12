using Game.Ads.Data;

namespace Gameplay.SaveAndLoadServices
{
    public interface ISaveSystem
    {
        void Save(SaveData saveData);
    }
}