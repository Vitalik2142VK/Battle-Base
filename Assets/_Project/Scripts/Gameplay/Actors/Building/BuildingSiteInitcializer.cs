using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Gameplay.Actors.Types;
using System;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSiteInitcializer : MonoBehaviour
    {
        [SerializeField] private ActorConfig _config;
        [SerializeField] private BuildingSite[] _buildingSites;

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] private bool _isEnableEnemySites = false;
#endif

        private IActorComposer _composer;
        private IBuildingSitesHandler _handler;

        [Inject]
        public void Construct(IActorComposer composer, IBuildingSitesHandler handler)
        {
            _composer = composer ?? throw new ArgumentNullException(nameof(composer));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
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

            if (buildingSite.TryGetComponent(out ActorViewSpawner actorViewSpawner) == false)
                throw new InvalidOperationException($"{nameof(buildingSite)} don't constrain component {nameof(ActorViewSpawner)}");

            TeamType team = buildingSite.Team;
            Actor actor = _composer.Compose(view, _config, team);

            RegisterBuildingSite(actor, buildingSite);
            InitEnemyBuildingSite(buildingSite, team);
        }

        private void RegisterBuildingSite(Actor actor, BuildingSite buildingSite)
        {
            if (actor.TryGetComponent(out IActorSpawner actorSpawner) == false)
                throw new InvalidOperationException($"{nameof(actor)} don't constrain component {nameof(ActorSpawner)}");

            _handler.Register(buildingSite, actorSpawner);
        }

        private void InitEnemyBuildingSite(BuildingSite buildingSite, TeamType team)
        {
            if (team != TeamType.Enemy)
                return;

#if UNITY_EDITOR
            if (_isEnableEnemySites)
                return;
#endif

            buildingSite.IstablishInactiveState();
        }
    }
}