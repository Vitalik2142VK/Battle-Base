using BattleBase.Gameplay.Actors.Availability;
using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Gameplay.Actors.Types;
using BattleBase.Utils;
using System;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSiteInitcializer : MonoBehaviour
    {
        [SerializeField] private ActorConfig _config;
        [SerializeField] private BuildingSite[] _buildingSites;

        private IActorComposer _composer;
        private IBuildingSitesStorage _storage;
        private IBuildingSiteIdCreator _idCreator;
        private IAvailabilityActorsRegistry _availabilityActors;

        [Inject]
        public void Construct(
            IActorComposer composer, 
            IBuildingSitesStorage storage, 
            IBuildingSiteIdCreator idCreator,
            IAvailabilityActorsRegistry availabilityActors)
        {
            _composer = composer ?? throw new ArgumentNullException(nameof(composer));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _idCreator = idCreator ?? throw new ArgumentNullException(nameof(idCreator));
            _availabilityActors = availabilityActors ?? throw new ArgumentNullException(nameof(availabilityActors));
        }

        private void Start()
        {
            foreach (var buildingSite in _buildingSites)
                InitBuildingSite(buildingSite);
        }

        private void InitBuildingSite(BuildingSite buildingSite)
        {
            if (buildingSite.TryGetComponent(out ActorView view) == false)
                throw new InvalidOperationException($"{nameof(buildingSite)} don't constrain component {nameof(ActorView)}");

            TeamType team = buildingSite.Team;
            Actor siteActor = _composer.Compose(view, _config, team);

            if (siteActor.TryGetComponent(out IActorSpawner spawner) == false)
                throw new InvalidOperationException($"{siteActor} don't constrain component {nameof(IActorSpawner)}");

            _availabilityActors.EstablishActors(team, spawner);
            buildingSite.Init(_idCreator);
            RegisterBuildingSite(siteActor, buildingSite);
            InitEnemyBuildingSite(buildingSite, team);
        }

        private void RegisterBuildingSite(Actor actor, BuildingSite buildingSite) =>
            _storage.Register(actor, buildingSite);

        private void InitEnemyBuildingSite(BuildingSite buildingSite, TeamType team)
        {
#if UNITY_EDITOR
            if (DebugSetting.IsAiDisbale) //todo remove on release
                return;
#endif
            if (team != TeamType.Enemy)
                return;

            buildingSite.EstablishInactiveState();
        }
    }
}