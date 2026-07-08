namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IMaterialCreator : IActorComponent, IUpdateable
    {
        public bool CanIncreaseProduction { get; }

        public void Init(ITeamable teamable);

        public void IncreaseProduction();
    }
}