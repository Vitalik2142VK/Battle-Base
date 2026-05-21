using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Gameplay.Actors.Movement;
using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Gameplay.Actors.Weapons;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ActorsLifetimeScope : LifetimeScope
{
    [SerializeField] private ActorPoolsRegistrator _poolsRegistrator;
    [SerializeField] private ActorsController _actorController;

    private IContainerBuilder _builder;

    protected override void Configure(IContainerBuilder builder)
    {
        _builder = builder ?? throw new System.ArgumentNullException(nameof(builder));

        _builder.RegisterComponent<IActorPoolsRegistrator>(_poolsRegistrator);
        _builder.RegisterInstance<IActorsController>(_actorController);
        
        _builder.Register<IActorSpawnService, ActorSpawnService>(Lifetime.Scoped);
        _builder.Register<IActorPoolRegistry, ActorPoolRegistry>(Lifetime.Scoped);

        _builder.Register<IComponentFactory, HealthFactory>(Lifetime.Scoped);
        _builder.Register<IComponentFactory, WeaponFactory>(Lifetime.Scoped);
        _builder.Register<IComponentFactory, MoverFactory>(Lifetime.Scoped);
        _builder.Register<IComponentFactory, ActorSpawnerFactory>(Lifetime.Scoped);
        _builder.Register<IComponentFactoryRegistry, ComponentFactoryRegistry>(Lifetime.Scoped);

        _builder.Register<IActorComponentBinder, HealthBinder>(Lifetime.Scoped);
        _builder.Register<IActorComponentBinder, WeaponBinder>(Lifetime.Scoped);
        _builder.Register<IActorComponentBinder, MoverBinder>(Lifetime.Scoped);
        _builder.Register<IActorComponentBinder, ActorSpawnerBinder>(Lifetime.Scoped);
        _builder.Register<IActorBinderRegistry, ActorBinderRegistry>(Lifetime.Scoped);
    }
}
