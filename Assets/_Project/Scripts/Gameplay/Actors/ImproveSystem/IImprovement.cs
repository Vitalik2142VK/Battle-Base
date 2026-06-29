using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImprovement : IActorComponent
    {
        public IImprovementData Data { get; }

        public bool CanImprove { get; }

        public void Init(IProductionData currentData);

        public void Improve();
    }
}