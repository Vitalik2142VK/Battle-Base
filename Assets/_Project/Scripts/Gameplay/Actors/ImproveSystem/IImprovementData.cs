using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImprovementData : IProductionData
    {
        public float PriceCoefficient { get; }
    }
}