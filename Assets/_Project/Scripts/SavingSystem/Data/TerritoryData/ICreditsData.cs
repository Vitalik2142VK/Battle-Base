namespace BattleBase.SaveService
{
    public interface ICreditsData : IChangeTrackable<ICreditsData>
    {
        public int Credits { get; }
    }
}