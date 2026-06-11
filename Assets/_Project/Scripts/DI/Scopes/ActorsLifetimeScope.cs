using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.AI;
using BattleBase.Gameplay.Actors.AI.Transition;
using BattleBase.Gameplay.Actors.AttackSystem;
using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Gameplay.Actors.Movement;
using BattleBase.Gameplay.Actors.Spawn;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleBase.DI
{
    public class ActorsLifetimeScope : LifetimeScope
    {
        [SerializeField] private ActorPoolsRegistrator _poolsRegistrator;
        [SerializeField] private ActorsController _actorController;
        [SerializeField] private WaypointController _waypointController;
        [SerializeField] private ProjectileSpawner _projectileSpawner;
        [SerializeField] private ColorGetter _colorGetter;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder ?? throw new System.ArgumentNullException(nameof(builder));

            _builder.RegisterComponent<IActorPoolsRegistrator>(_poolsRegistrator);
            _builder.RegisterInstance<IActorsController>(_actorController);
            _builder.RegisterInstance<IWaypointController>(_waypointController);
            _builder.RegisterInstance<IProjectileSpawner>(_projectileSpawner);
            _builder.RegisterInstance<IColorGetter>(_colorGetter);

            _builder.Register<IActorSpawnService, ActorSpawnService>(Lifetime.Scoped);
            _builder.Register<IActorColorService, ActorColorService>(Lifetime.Scoped);
            _builder.Register<IActorPoolRegistry, ActorPoolRegistry>(Lifetime.Scoped);
            _builder.Register<IActorComposer, ActorComposer>(Lifetime.Scoped);

            _builder.Register<IComponentFactory, HealthFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, AttackerFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, MoverFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, ActorSpawnerFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, MultiActorSpawnerFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, ActorStateMachineFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactoryRegistry, ComponentFactoryRegistry>(Lifetime.Scoped);

            _builder.Register<IActorComponentBinder, HealthBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, AttackerBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, MoverBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, ActorSpawnerBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, ColoredActorBinder>(Lifetime.Scoped);
            _builder.Register<IActorBinderRegistry, ActorBinderRegistry>(Lifetime.Scoped);

            _builder.Register<IStateTransitionFactory, AttackStateTransitionFactory>(Lifetime.Scoped);
            _builder.Register<IStateTransitionFactory, AttackToMoveStateTransitionFactory>(Lifetime.Scoped);
            _builder.Register<IStateMachineInitializer, StateMachineInitializer>(Lifetime.Scoped);
        }
    }
}