namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IPowerGenerator : IActorComponent
    {
        public bool CanIncreasePower { get; }

        public void Init(ITeamable teamable);

        public void IncreasePower();
    }
}
