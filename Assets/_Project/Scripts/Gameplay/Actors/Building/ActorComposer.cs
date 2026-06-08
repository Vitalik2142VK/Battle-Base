using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Building
{
    public class ActorComposer : IActorComposer
    {
        private readonly IActorsController _actorsController;
        private readonly IComponentFactoryRegistry _componentFactoryRegistry;
        private readonly IActorBinderRegistry _actorBinderRegistry;
        private readonly IActorColorService _colorService;

        public ActorComposer(
            IActorsController actorsController,
            IComponentFactoryRegistry componentFactoryRegistry, 
            IActorBinderRegistry actorBinderRegistry, 
            IActorColorService colorService)
        {
            _actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
            _componentFactoryRegistry = componentFactoryRegistry ?? throw new ArgumentNullException(nameof(componentFactoryRegistry));
            _actorBinderRegistry = actorBinderRegistry ?? throw new ArgumentNullException(nameof(actorBinderRegistry));
            _colorService = colorService ?? throw new ArgumentNullException(nameof(colorService));
        }

        public Actor Compose(ActorView view, IActorConfig config, TeamType team)
        {
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

                if (component is IDestroyableEvents damagebleEvents)
                    builder.DamagebleEvents(damagebleEvents);
            }

            Actor actor = builder.Build();
            _actorBinderRegistry.Bind(actor, view);
            _actorsController.AddActor(actor);

            actor.Enable();
            actor.SetTeam(team);

            _colorService.EstabilshColor(actor, view);

            return actor;
        }
    }
}