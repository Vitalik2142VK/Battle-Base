namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IAdvancedPowerRegistry : IPowerRegistry
    {
        public void AddCapacity(TeamType team, int capacity);

        public void Release(TeamType team, int capacity);
    }
}
