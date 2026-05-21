using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorPoolRegistry : IActorPoolRegistry
    {
        private readonly IActorPoolsRegistrator _actorPoolsRegistrator;

        public ActorPoolRegistry(IActorPoolsRegistrator actorPoolsRegistrator)
        {
            _actorPoolsRegistrator = actorPoolsRegistrator ?? throw new ArgumentNullException(nameof(actorPoolsRegistrator));
        }

        public bool TryGive(out Actor actor, string namePrefab)
        {
            actor = null;

            if (_actorPoolsRegistrator.Pools.TryGetValue(namePrefab, out ActorPool pool)) 
            {
                if (pool.TryGive(out actor))
                    return true;
            }

            return false;
        }
    }
}