using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class EnemyAvailabilityActors : IAvailabilityActors
    {
        private readonly AvailabilityActors _availabilityActors;

        public EnemyAvailabilityActors(IEnemyAvailableActorProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            _availabilityActors = new AvailabilityActors(provider.ActualConfigs, TeamType.Enemy);
        }

        public TeamType Team => _availabilityActors.Team;

        public void EstablishActors(IActorSpawner spawner) =>
            _availabilityActors.EstablishActors(spawner);
    }
}