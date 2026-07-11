namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImproverComponent : IImprover
    {
        public void Init(ITeamable teamable);
    }
}