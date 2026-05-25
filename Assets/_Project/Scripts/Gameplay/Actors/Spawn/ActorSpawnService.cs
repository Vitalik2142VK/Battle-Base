using BattleBase.Gameplay.Actors.Movement;
using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawnService : IActorSpawnService
    {
        private readonly IActorPoolRegistry _poolRegistry;
        private readonly IActorsController _actorsController;
        private readonly IWaypointController _waypointController;

        public ActorSpawnService(
            IActorPoolRegistry poolRegistry, 
            IActorsController actorsController,
            IWaypointController waypointController)
        {
            _poolRegistry = poolRegistry ?? throw new ArgumentNullException(nameof(poolRegistry));
            _actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
            _waypointController = waypointController ?? throw new ArgumentNullException(nameof(waypointController));
        }

        public bool TrySpawn(string prefabName, ISpawnData spawnData, out Actor actor)
        {
            if (string.IsNullOrEmpty(prefabName))
                throw new ArgumentException($"{nameof(prefabName)} cannot be empty or null");

            if (spawnData == null)
                throw new ArgumentNullException(nameof(spawnData));

            if (_poolRegistry.TryGive(out actor, prefabName) == false)
            {
                return false;
            }

            _actorsController.AddActor(actor);
            _waypointController.SpecifyActorRoute(actor);

            return true;
        }
    }
}