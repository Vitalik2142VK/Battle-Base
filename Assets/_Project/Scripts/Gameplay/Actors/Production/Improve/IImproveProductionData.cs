using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.ImproveSystem;

namespace BattleBase.Gameplay.Actors.Production.Improve
{
    public interface IImproveProductionData : IProductionData, IImproverState
    {
        public IMaterialData MaterialData { get; }
    }
}