using YG;

namespace BattleBase.SaveService
{
    public class YandexGameSaveSystemAdapter : ISaveSystem
    {
        public SavesData Data => YG2.saves.SavesData;

        public void SaveProgress() =>
            YG2.SaveProgress();
    }
}