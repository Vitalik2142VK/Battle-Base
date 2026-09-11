namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IAdvancedPowerRegistry : IPowerRegistry
    {
        public void AddCapacity(TeamType team, int capacity);

        public void ReduceCapacity(TeamType team, int capacity);

        public void Reserve(TeamType team, IActorData actorData);

        public void Release(TeamType team, IActorData actorData);
    }
}
