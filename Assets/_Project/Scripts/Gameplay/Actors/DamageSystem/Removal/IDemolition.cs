using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public interface IDemolition : IActorComponent, IDestroyableEvent
    {
        public IDemolitionData Data { get; }

        public void Init(IProductionData currentData);

        public void Demolish();
    }
}