using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImprover : IActorComponent
    {
        public IImproverData Data { get; }

        public bool CanImprove { get; }

        public void Init(IProductionData currentData);

        public void Improve();
    }
}