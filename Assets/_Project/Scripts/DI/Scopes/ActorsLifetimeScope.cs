using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.AI;
using BattleBase.Gameplay.Actors.AI.Transition;
using BattleBase.Gameplay.Actors.AttackSystem;
using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.DamageSystem.Modifiers;
using BattleBase.Gameplay.Actors.DamageSystem.Removal;
using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Gameplay.Actors.ImproveSystem;
using BattleBase.Gameplay.Actors.Movement;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Gameplay.Actors.Visual.Particle;
using BattleBase.Gameplay.AI;
using BattleBase.Gameplay.Levels;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static BattleBase.Gameplay.Actors.DamageSystem.Removal.DemolitionBinder;

namespace BattleBase.DI
{
    public class ActorsLifetimeScope : LifetimeScope
    {
        [SerializeField] private ActorPoolsRegistrator _poolsRegistrator;
        [SerializeField] private ActorsController _actorController;
        [SerializeField] private WaypointController _waypointController;
        [SerializeField] private ProjectileSpawner _projectileSpawner;
        [SerializeField] private TrailParticleSpawner _trailParticleSpawner;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder ?? throw new System.ArgumentNullException(nameof(builder));

            _builder.RegisterComponent<IActorPoolsRegistrator>(_poolsRegistrator);
            _builder.RegisterInstance<IActorsController>(_actorController);
            _builder.RegisterInstance<IWaypointController>(_waypointController);
            _builder.RegisterInstance<IProjectileSpawner>(_projectileSpawner);
            _builder.RegisterInstance<ITrailParticleSpawner>(_trailParticleSpawner);

            _builder.Register<IActorSpawnService, ActorSpawnService>(Lifetime.Scoped);
            _builder.Register<IActorColorService, ActorColorService>(Lifetime.Scoped);
            _builder.Register<IActorPoolRegistry, ActorPoolRegistry>(Lifetime.Scoped);
            _builder.Register<IActorComposer, ActorComposer>(Lifetime.Scoped);
            _builder.Register<IDamageModifierFactory, DamageModifierFactory>(Lifetime.Scoped);
            _builder.Register<IWinStateController, WinStateController>(Lifetime.Scoped);
            _builder.Register<IBuildingSitesController, BuildingSitesController>(Lifetime.Scoped);
            _builder.Register<IActorCreator, ActorCreator>(Lifetime.Scoped);

            RegisterComponentFactoryRegistry();
            RegisterActorBinderRegistry();
            RegisterActorConnectorRegistry();
            RegisterStateMachineInitializer();
            RegisterAI();
        }

        private void RegisterComponentFactoryRegistry()
        {
            _builder.Register<IComponentFactory, HealthFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, AttackerFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, MoverFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, SingleActorSpawnerFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, MultiActorSpawnerFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, ActorStateMachineFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, ImprovementFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, DemolitionFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactory, ProductionServiceFactory>(Lifetime.Scoped);
            _builder.Register<IComponentFactoryRegistry, ComponentFactoryRegistry>(Lifetime.Scoped);
        }

        private void RegisterActorBinderRegistry()
        {
            _builder.Register<IActorComponentBinder, HealthBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, AttackerBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, MoverBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, ActorSpawnerBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, ColoredActorBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, ImprovementBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, DemolitionBinder>(Lifetime.Scoped);
            _builder.Register<IActorComponentBinder, ProductionServiceBinder>(Lifetime.Scoped);
            _builder.Register<IActorBinderRegistry, ActorBinderRegistry>(Lifetime.Scoped);
        }

        private void RegisterActorConnectorRegistry()
        {
            _builder.Register<IActorComponentConnector, ProductionServiceConnector>(Lifetime.Scoped);
            _builder.Register<IActorComponentConnector, DemolitionServiceConnector>(Lifetime.Scoped);
            _builder.Register<IActorConnectorRegistry, ActorConnectorRegistry>(Lifetime.Scoped);
        }

        private void RegisterStateMachineInitializer()
        {
            _builder.Register<IStateTransitionFactory, AttackStateTransitionFactory>(Lifetime.Scoped);
            _builder.Register<IStateTransitionFactory, AttackToMoveStateTransitionFactory>(Lifetime.Scoped);
            _builder.Register<IStateMachineInitializer, StateMachineInitializer>(Lifetime.Scoped);
        }

        private void RegisterAI()
        {
            _builder.Register<IBrain, Brain>(Lifetime.Scoped);
            _builder.Register<ITactic, RandomTactic>(Lifetime.Scoped);
            _builder.Register<RandomTacticSetting>(Lifetime.Scoped).WithParameter(TeamType.Enemy);
        }
    }
}