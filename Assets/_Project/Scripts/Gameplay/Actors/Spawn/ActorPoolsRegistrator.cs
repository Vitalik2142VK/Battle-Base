using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorPoolsRegistrator : MonoBehaviour, IActorPoolsRegistrator
    {
        [SerializeField] private ActorSpawnerSource[] _spawnerSources;

        public IDictionary<string, ActorPool> Pools { get; private set; }

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver));

            Pools = new Dictionary<string, ActorPool>();

            IActorCreator actorCreator = resolver.Resolve<IActorCreator>();

            foreach (var spawnerSource in _spawnerSources)
                RegisryPools(spawnerSource.ActorsConfigs, resolver, actorCreator);
        }

        private void RegisryPools(IEnumerable<IActorConfig> actorsConfigs, IObjectResolver resolver, IActorCreator actorCreator)
        {
            foreach (var config in actorsConfigs)
            {
                ActorFactory factory = new(config, resolver, actorCreator);

                // todo: Constants.PoolMaximumSize it doesn't hurt anymore, it may need to be moved to the config
                int tempPoolSize = int.MaxValue;
                ActorPool pool = new(factory, tempPoolSize);

                Pools.Add(config.Data.Id, pool);
            }
        }
    }
}