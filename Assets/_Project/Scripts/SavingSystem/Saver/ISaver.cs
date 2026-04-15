namespace BattleBase.SaveService
{
    public interface ISaver
    {
        public void Save();

        public void ResetProgress();
    }
}