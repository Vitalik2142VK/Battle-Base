namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IMaterialCreator : IActorComponent, IUpdateable
    {
        public void Init(ITeamable teamable);
    }
}