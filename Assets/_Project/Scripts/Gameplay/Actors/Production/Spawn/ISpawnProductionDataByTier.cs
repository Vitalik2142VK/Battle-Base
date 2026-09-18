namespace BattleBase.Gameplay.Actors.Production.Spawn
{
    public interface ISpawnProductionDataByTier : ISpawnProductionData
    {
        public int Tier { get; }
    }
}