using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class PlayerAvailabilityActors : IAvailabilityActors
    {
        private readonly AvailabilityActors _availabilityActors;

        public PlayerAvailabilityActors(IPlayerAvailableActorProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            _availabilityActors = new AvailabilityActors(provider.ActualConfigs, TeamType.Player);
        }

        public TeamType Team => _availabilityActors.Team;

        public void EstablishActors(IActorSpawner spawner) =>
            _availabilityActors.EstablishActors(spawner);
    }
}