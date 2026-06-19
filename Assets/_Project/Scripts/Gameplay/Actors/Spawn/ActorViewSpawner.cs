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

        public IEnumerable<IActorData> ActorsData => _presenter.ActorsDatas;

        public Vector3 SpawnPosition => _spawnPoint.position;

        public Quaternion SpawnRotation => _spawnPoint.rotation;

        private void OnValidate()
        {
            if (_spawnPoint == null)
                _spawnPoint = transform;
        }

        public void Init(IActorSpawnerPresenter presenter)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public void SelectActorData(IActorData actorData) =>
            _presenter.SendActorData(actorData);
    }
}
