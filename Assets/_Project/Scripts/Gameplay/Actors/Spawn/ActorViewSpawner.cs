using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    [RequireComponent(typeof(Collider))]
    public class ActorViewSpawner : MonoBehaviour, IActorViewSpawner
    {
        [SerializeField] private Transform _spawnPoint;
        //[SerializeField] private Renderer _renderer;

        private IActorSpawnerPresenter _presenter;
        private IActorSpawnerEvents _events;

        public IEnumerable<IActorData> ActorsData => _presenter.ActorsDatas;

        public IBuildingSite BuildingSite { get; private set; }

        private void OnValidate()
        {
            //todo remove or change
            //if (TryGetComponent(out Renderer renderer))
            //    _renderer = renderer;

            if (_spawnPoint == null)
                _spawnPoint = transform;
        }

        private void OnEnable()
        {
            if (_events != null)
                _events.Spawned += OnSpawn;
        }

        private void OnDisable()
        {
            if (_events != null)
                _events.Spawned -= OnSpawn;
        }

        public void Init(IActorSpawnerPresenter presenter, IActorSpawnerEvents events)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _events = events ?? throw new ArgumentNullException(nameof(events));

            if (gameObject.activeSelf)
                _events.Spawned += OnSpawn;
        }

        public void SelectActorData(IActorData actorData) =>
            _presenter.SendActorData(actorData);

        public void SetBuildingSite(IBuildingSite buildingSite)
        {
            BuildingSite = buildingSite ?? throw new ArgumentNullException(nameof(buildingSite));
        }

        private void OnSpawn(Actor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            EstablishTransform(actor.View);
        }

        private void EstablishTransform(IActorView view)
        {
            if (view is MonoBehaviour actor == false)
                throw new InvalidOperationException();

            var unitTransform = actor.transform;
            unitTransform.SetPositionAndRotation(_spawnPoint.position, _spawnPoint.rotation);
            unitTransform.gameObject.SetActive(true);

            if (view.TryGetViewComponent(out IActorViewSpawner component))
            {
                BuildingSite.SetInactiveState();
                component.SetBuildingSite(BuildingSite);
            }

            //todo Delete after the selected color is implemented ...
            //var renderersActor = GetComponentsInChildren<MeshRenderer>(true);

            //foreach (var renderer in renderersActor)
            //    renderer.material = _renderer.material;
            //...
        }
    }
}
