namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IAdvancedPowerRegistry : IPowerRegistry
    {
        public void AddCapacity(TeamType team, int capacity);

        public void ReduceCapacity(TeamType team, int capacity);

        public void Release(TeamType team, IActorData actorData);
    }
}
