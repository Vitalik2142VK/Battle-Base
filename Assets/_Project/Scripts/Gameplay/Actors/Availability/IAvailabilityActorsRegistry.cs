using BattleBase.Gameplay.Actors.Spawn;

namespace BattleBase.Gameplay.Actors.Availability
{
    public interface IAvailabilityActorsRegistry
    {
        public void EstablishActors(TeamType team, IActorSpawner spawner);
    }
}