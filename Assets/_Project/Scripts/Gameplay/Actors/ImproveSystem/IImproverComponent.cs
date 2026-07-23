namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImproverComponent : IImprover, IImproverEvents
    {
        public void Init(ITeamable teamable);
    }
}