namespace BattleBase.SaveService
{
    public interface ICreditsSaver
    {
        public ICreditsData CreditsData { get; }

        public void SetCreditsData(ICreditsData data);
    }
}