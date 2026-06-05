using BattleBase.Gameplay.Actors.AI;
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
                IStateMachineInitializer stateMachineInitializer = resolver.Resolve<IStateMachineInitializer>();

                ActorFactory factory = new(
                    config,
                    componentFactoryRegistry,
                    actorBinderRegistry,
                    resolver,
                    stateMachineInitializer);

                // todo: Constants.PoolMaximumSize it doesn't hurt anymore
                // it may need to be moved to the config
                int tempPoolSize = int.MaxValue;
                ActorPool pool = new(factory, tempPoolSize);

                Pools.Add(config.Data.Prefab.name, pool);
            }
        }
    }
}