namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public interface IDemolition : IActorComponent, IDestroyableEvent
    {
        public IDemolitionData Data { get; }

        public void Init(IPriceCounterDemolition priceCounter, ITeamable teamable);

        public void Demolish();
    }
}