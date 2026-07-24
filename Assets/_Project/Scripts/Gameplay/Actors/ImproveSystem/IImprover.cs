using BattleBase.Gameplay.Actors.Production.Improve;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImprover : IActorComponent
    {
        public IImproveProductionData Data { get; }

        public bool CanImprove { get; }

        public bool TryImprove();
    }
}