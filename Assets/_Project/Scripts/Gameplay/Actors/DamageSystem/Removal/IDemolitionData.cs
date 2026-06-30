using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public interface IDemolitionData : IProductionData
    {
        public float ReturnedCoefficient { get; }
    }
}