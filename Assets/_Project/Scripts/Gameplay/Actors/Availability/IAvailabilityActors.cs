using BattleBase.Gameplay.Actors.Spawn;

namespace BattleBase.Gameplay.Actors.Availability
{
    public interface IAvailabilityActors
    {
        public TeamType Team { get; }

        public void EstablishActors(IActorSpawner spawner);
    }
}