namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IMaterialConfig
    {
        public int StartMaterialsCount { get; }

        public int MaxCapacity { get; }
    }
}