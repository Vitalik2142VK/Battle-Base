using BattleBase.Gameplay.Actors.Spawn;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class EnemyAvailabilityActors : IAvailabilityActors
    {
        public TeamType Team => TeamType.Enemy;

        public void EstablishActors(IActorSpawner spawner)
        {
            UnityEngine.Debug.LogWarning($"Implement class {nameof(EnemyAvailabilityActors)}");
        }
    }
}