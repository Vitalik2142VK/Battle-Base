using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImprover : IActorComponent
    {
        public IProductionData Data { get; }

        public bool CanImprove { get; }

        public void Improve();
    }
}