using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImproverData : IProductionData
    {
        public float PriceCoefficient { get; }
    }
}