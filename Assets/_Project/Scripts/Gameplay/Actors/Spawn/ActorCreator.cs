using BattleBase.Gameplay.Actors.AI;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorCreator : IActorCreator
    {
        private readonly IComponentFactoryRegistry _componentFactoryRegistry;
        private readonly IActorBinderRegistry _actorBinderRegistry;
        private readonly IStateMachineInitializer _stateMachineInitializer;

        public ActorCreator(
            IComponentFactoryRegistry componentFactoryRegistry,
            IActorBinderRegistry actorBinderRegistry,
            IStateMachineInitializer stateMachineInitializer)
        {
            _componentFactoryRegistry = componentFactoryRegistry;
            _actorBinderRegistry = actorBinderRegistry;
            _stateMachineInitializer = stateMachineInitializer;
        }

        public Actor Create(ActorView view, IActorConfig config)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            view.Init();

            ActorBuilder builder = new();
            builder
                .ActorView(view)
                .ActorData(config.Data);

            IEnumerable<IComponentSource> componentSources = config.GetComponentSources();

            foreach (var componentSource in componentSources)
            {
                IActorComponent component = _componentFactoryRegistry.Create(componentSource);
                builder.AddComponent(component);

                if (component is IDestroyableEvent destroyableEvent)
                    builder.AddDestroyableEvent(destroyableEvent);
            }

            Actor actor = builder.Build();
            _stateMachineInitializer.Initialize(actor);
            _actorBinderRegistry.Bind(actor, view);

            return actor;
        }
    }
}