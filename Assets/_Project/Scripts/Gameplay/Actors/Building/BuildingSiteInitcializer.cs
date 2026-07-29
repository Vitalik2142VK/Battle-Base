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

        [Inject]
        public void Construct(IActorComposer composer, IBuildingSitesStorage storage)
        {
            _composer = composer ?? throw new ArgumentNullException(nameof(composer));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
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
            Actor actor = _composer.Compose(view, _config, team);

            RegisterBuildingSite(actor, buildingSite);
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