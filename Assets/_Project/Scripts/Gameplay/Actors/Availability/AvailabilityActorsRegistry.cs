using BattleBase.Gameplay.Actors.Spawn;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class AvailabilityActorsRegistry : IAvailabilityActorsRegistry
    {
        private readonly Dictionary<TeamType, IAvailabilityActors> _availabilityActors;

        public AvailabilityActorsRegistry(IEnumerable<IAvailabilityActors> availabilityActors)
        {
            if (availabilityActors == null)
                throw new System.ArgumentNullException(nameof(availabilityActors));

            _availabilityActors = new Dictionary<TeamType, IAvailabilityActors>();

            foreach (var availabilityActor in availabilityActors)
                _availabilityActors.Add(availabilityActor.Team, availabilityActor);
        }

        public void EstablishActors(TeamType team, IActorSpawner spawner) =>
            _availabilityActors[team].EstablishActors(spawner);
    }
}