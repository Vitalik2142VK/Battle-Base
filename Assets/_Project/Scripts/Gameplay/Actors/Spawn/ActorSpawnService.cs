using BattleBase.Gameplay.Actors.ComponentImprovement;
using BattleBase.Gameplay.Actors.Movement;
using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawnService : IActorSpawnService
    {
        private readonly IActorPoolRegistry _poolRegistry;
        private readonly IActorsController _actorsController;
        private readonly IWaypointController _waypointController;
        private readonly IActorUpgraderRegistry _actorComponentUpgrader;

        public ActorSpawnService(
            IActorPoolRegistry poolRegistry,
            IActorsController actorsController,
            IWaypointController waypointController,
            IActorUpgraderRegistry actorComponentUpgrader)
        {
            _poolRegistry = poolRegistry ?? throw new ArgumentNullException(nameof(poolRegistry));
            _actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
            _waypointController = waypointController ?? throw new ArgumentNullException(nameof(waypointController));
            _actorComponentUpgrader = actorComponentUpgrader ?? throw new ArgumentNullException(nameof(actorComponentUpgrader));
        }

        public Actor Spawn(string id, TeamType teamType, ISpawnPoint spawnData)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"{nameof(id)} cannot be empty or null");

            if (spawnData == null)
                throw new ArgumentNullException(nameof(spawnData));

            if (_poolRegistry.TryGive(out Actor actor, id) == false)
                throw new InvalidOperationException($"{nameof(_poolRegistry)} has run out of space");

            actor.SetSpawnData(spawnData);
            actor.SetTeam(teamType);

            if (actor.TryGetComponent(out IMover mover))
                _waypointController.SpecifyActorRoute(mover, spawnData);

            _actorComponentUpgrader.UpgradeActorComponents(teamType, actor);
            _actorsController.AddActor(actor);

            return actor;
        }
    }
}