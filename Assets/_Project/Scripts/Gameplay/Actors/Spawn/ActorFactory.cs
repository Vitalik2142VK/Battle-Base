using BattleBase.Core;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorFactory : IFactory<Actor>
    {
        private readonly IObjectResolver _resolver;
        private readonly IComponentFactoryRegistry _componentFactoryRegistry;
        private readonly IActorBinderRegistry _actorBinderRegistry;
        private readonly ActorConfig _config;

        private int _unitCounter;

        public ActorFactory(
            ActorConfig config,
            IComponentFactoryRegistry componentFactoryRegistry,
            IActorBinderRegistry actorBinderRegistry,
            IObjectResolver resolver)
        {
            _config = config != null ? config : throw new ArgumentNullException(nameof(config));
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _componentFactoryRegistry = componentFactoryRegistry ?? throw new ArgumentNullException(nameof(componentFactoryRegistry));
            _actorBinderRegistry = actorBinderRegistry ?? throw new ArgumentNullException(nameof(actorBinderRegistry));

            _unitCounter = 0;
        }

        public Actor Create()
        {
            ActorView prefab = _config.Data.Prefab;
            prefab.name = $"{prefab.name}_{++_unitCounter}";

            ActorView view = _resolver.Instantiate(prefab);
            view.Init();

            ActorBuilder builder = new();
            builder
                .ActorView(view)
                .ActorData(_config.Data);

            IEnumerable<IComponentSource> componentSources = _config.GetComponentSources();

            foreach (var componentSource in componentSources)
            {
                IActorComponent component = _componentFactoryRegistry.Create(componentSource);
                builder.AddComponent(component);

                if (component is IDestroyableEvents damagebleEvents)
                    builder.DamagebleEvents(damagebleEvents);
            }

            Actor actor = builder.Build();

            _actorBinderRegistry.Bind(actor, view);

            return actor;
        }
    }
}