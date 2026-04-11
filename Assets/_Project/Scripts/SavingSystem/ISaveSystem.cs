namespace BattleBase.SaveService
{
    public interface ISaveSystem
    {
        public SavesData Data {  get; }

        public void SaveProgress();
    }
}