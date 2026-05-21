using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawnService : IActorSpawnService
    {
        private readonly IActorPoolRegistry _poolRegistry;
        private readonly IActorsController _actorsController;

        public ActorSpawnService(IActorPoolRegistry poolRegistry, IActorsController actorsController)
        {
            _poolRegistry = poolRegistry ?? throw new ArgumentNullException(nameof(poolRegistry));
            _actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
        }

        public bool TrySpawn(string prefabName, TeamType teamType, out Actor actor)
        {
            if (_poolRegistry.TryGive(out actor, prefabName) == false)
                return false;

            actor.Enable();
            actor.SetTeam(teamType);

            _actorsController.AddActor(actor);

            return true;
        }
    }
}