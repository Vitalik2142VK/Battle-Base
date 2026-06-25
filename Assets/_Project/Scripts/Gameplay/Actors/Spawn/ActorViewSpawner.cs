using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    [RequireComponent(typeof(Collider))]
    public class ActorViewSpawner : MonoBehaviour, IActorViewSpawner
    {
        [SerializeField] private Transform _spawnPoint;

        private IActorSpawnerPresenter _presenter;
        private ITeamable _teamable;

        public IEnumerable<ProductionOption> ProductionOptions => _presenter.ProductionOptions;

        public TeamType TeamType => _teamable.TeamType;

        public Vector3 SpawnPosition => _spawnPoint.position;

        public Quaternion SpawnRotation => _spawnPoint.rotation;

        private void OnValidate()
        {
            if (_spawnPoint == null)
                _spawnPoint = transform;
        }

        public void Init(IActorSpawnerPresenter presenter, ITeamable teamable)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
        }
    }
}
