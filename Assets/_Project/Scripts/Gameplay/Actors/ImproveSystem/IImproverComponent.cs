namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImproverComponent : IImprover, IImproverINotifier
    {
        public void Init(ITeamable teamable);
    }
}