using BattleBase.Gameplay.Actors.Spawn;
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

        [Inject]
        public void Construct(IActorComposer composer)
        {
            _composer = composer ?? throw new ArgumentNullException(nameof(composer));
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

            if (buildingSite.TryGetComponent(out IActorViewSpawner actorViewSpawner) == false)
                throw new InvalidOperationException($"{nameof(buildingSite)} don't constrain component {nameof(IActorViewSpawner)}");

            _composer.Compose(view, _config, buildingSite.Team);
            actorViewSpawner.SetBuildingSite(buildingSite);
        }
    }
}