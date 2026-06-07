using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay
{
    public class BaseInitcializer : MonoBehaviour
    {
        [SerializeField] private ActorConfig _config;
        [SerializeField] private ActorView _playerBase;
        [SerializeField] private ActorView _enemyBase;

        private IComponentFactoryRegistry _componentFactoryRegistry;
        private IActorBinderRegistry _actorBinderRegistry;
        private IActorsController _actorsController;

        [Inject]
        public void Construct(
            IComponentFactoryRegistry componentFactoryRegistry,
            IActorBinderRegistry actorBinderRegistry,
            IActorsController actorsController)
        {
            _componentFactoryRegistry = componentFactoryRegistry ?? throw new ArgumentNullException(nameof(componentFactoryRegistry));
            _actorBinderRegistry = actorBinderRegistry ?? throw new ArgumentNullException(nameof(actorBinderRegistry));
            _actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
        }

        private void Start()
        {
            InitBase(_playerBase);
            InitBase(_enemyBase);
        }

        //todo get rid of the copy paste in to BuildingSiteInitcializer
        private void InitBase(ActorView view)
        {
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
            _actorsController.AddActor(actor);

            actor.SetTeam(actor.TeamType);
            actor.Enable();
        }
    }
}