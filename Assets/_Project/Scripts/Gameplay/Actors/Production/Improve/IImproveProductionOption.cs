using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.ImproveSystem;

namespace BattleBase.Gameplay.Actors.Production.Improve
{
    public interface IImproveProductionOption : IProductionOption
    {
        public IMaterialData MaterialData { get; }

        public IImproverState ImproverState { get; }
    }
}