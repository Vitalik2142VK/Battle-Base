using BattleBase.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorPoolsRegistrator : MonoBehaviour, IActorPoolsRegistrator
    {
        [SerializeField] private ActorConfig[] _actorsConfigs;

        public IDictionary<string, ActorPool> Pools { get; private set; }

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver));

            Pools = new Dictionary<string, ActorPool>();

            foreach (var config in _actorsConfigs)
            {
                IComponentFactoryRegistry componentFactoryRegistry = resolver.Resolve<IComponentFactoryRegistry>();
                IActorBinderRegistry actorBinderRegistry = resolver.Resolve<IActorBinderRegistry>();

                ActorFactory factory = new(config, componentFactoryRegistry, actorBinderRegistry, resolver);
                ActorPool pool = new(factory, Constants.PoolMaximumSize);

                Pools.Add(config.Data.Prefab.name, pool);
            }
        }
    }
}