using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors
{
    public interface IActorData : IProductionData
    {
        public string Id { get; }

        public ActorView Prefab { get; }

        public int Power { get; }
    }
}