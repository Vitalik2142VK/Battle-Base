using BattleBase.Saves;

namespace BattleBase.Services.SaveService
{
    public interface ISaveSystem
    {
        public SavesData Data {  get; }

        public void SaveProgress();
    }
}