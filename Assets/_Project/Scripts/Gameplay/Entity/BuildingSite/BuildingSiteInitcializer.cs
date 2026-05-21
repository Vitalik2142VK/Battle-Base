using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay
{
    public class BuildingSiteInitcializer : MonoBehaviour
    {
        [SerializeField] private ActorConfig _config;
        [SerializeField] private BuildingSite[] _playerBuildingSites;
        [SerializeField] private BuildingSite[] _enemyBuildingSites;

        private IComponentFactoryRegistry _componentFactoryRegistry;
        private IActorBinderRegistry _actorBinderRegistry;
        private IActorsController _actorsController;

        [Inject]
        public void Constract(
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
            foreach (var buildingSite in _playerBuildingSites)
                InitBuildingSite(buildingSite, TeamType.Player);

            foreach (var buildingSite in _enemyBuildingSites)
                InitBuildingSite(buildingSite, TeamType.Enemy);
        }

        private void InitBuildingSite(BuildingSite buildingSite, TeamType teamType)
        {
            if (buildingSite.TryGetComponent(out ActorView view) == false)
                throw new InvalidOperationException($"{nameof(buildingSite)} don't constrain component {nameof(ActorView)}");

            if (buildingSite.TryGetComponent(out IActorViewSpawner actorViewSpawner) == false)
                throw new InvalidOperationException($"{nameof(buildingSite)} don't constrain component {nameof(IActorViewSpawner)}");

            view.Init();
            actorViewSpawner.SetBuildingSite(buildingSite);

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
            actor.SetTeam(teamType);
            actor.Enable();

            _actorBinderRegistry.Bind(actor, view);
            _actorsController.AddActor(actor);
        }
    }
}