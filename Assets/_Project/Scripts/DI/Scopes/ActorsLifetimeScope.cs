using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.AI;
using BattleBase.Gameplay.Actors.AI.Transition;
using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Gameplay.Actors.Movement;
using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Gameplay.Actors.AttackSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using BattleBase.Gameplay.Actors.AttackSystem.Missiles;

public class ActorsLifetimeScope : LifetimeScope
{
    [SerializeField] private ActorPoolsRegistrator _poolsRegistrator;
    [SerializeField] private ActorsController _actorController;
    [SerializeField] private WaypointController _waypointController;
    [SerializeField] private MissileSpawner _missileSpawner;

    private IContainerBuilder _builder;

    protected override void Configure(IContainerBuilder builder)
    {
        _builder = builder ?? throw new System.ArgumentNullException(nameof(builder));

        _builder.RegisterComponent<IActorPoolsRegistrator>(_poolsRegistrator);
        _builder.RegisterInstance<IActorsController>(_actorController);
        _builder.RegisterInstance<IWaypointController>(_waypointController);
        _builder.RegisterInstance<IMissileSpawner>(_missileSpawner);
        
        _builder.Register<IActorSpawnService, ActorSpawnService>(Lifetime.Scoped);
        _builder.Register<IActorPoolRegistry, ActorPoolRegistry>(Lifetime.Scoped);

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
        _builder.Register<IActorBinderRegistry, ActorBinderRegistry>(Lifetime.Scoped);

        _builder.Register<IStateTransitionFactory, AttackStateTransitionFactory>(Lifetime.Scoped);
        _builder.Register<IStateTransitionFactory, AttackToMoveStateTransitionFactory>(Lifetime.Scoped);
        _builder.Register<IStateMachineInitializer, StateMachineInitializer>(Lifetime.Scoped);
    }
}
