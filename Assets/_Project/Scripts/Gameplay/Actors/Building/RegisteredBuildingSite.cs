using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.Building
{
    public class RegisteredBuildingSite : IRegisteredBuildingSite
    {
        private readonly IActor _buildingSiteActor;
        private readonly IBuildingSite _buildingSite;
        private readonly IActorSpawnerNotifier _notifier;

        private IActor _currentActor;
        private IDestroyableEvent _destroyableEvents;

        public event Action<RegisteredBuildingSite> ActorAdded;
        public event Action<IRegisteredBuildingSite> ActorMissing;
        public event Action StateChanged;

        public RegisteredBuildingSite(IActor buildingSiteActor, IBuildingSite buildingSite)
        {
            _buildingSiteActor = buildingSiteActor ?? throw new ArgumentNullException(nameof(buildingSiteActor));
            _buildingSite = buildingSite ?? throw new ArgumentNullException(nameof(buildingSite));
            _currentActor = _buildingSiteActor;

            if (_buildingSiteActor.TryGetComponent(out IActorSpawner actorSpawner) == false)
                throw new InvalidOperationException($"{nameof(buildingSiteActor)} don't constrain component {nameof(IActorSpawner)}");

            if (_buildingSiteActor.TryGetComponent(out IProductionService productionService) == false)
                throw new InvalidOperationException($"{nameof(buildingSiteActor)} don't constrain component {nameof(IProductionService)}");

            productionService.SetBuildingSiteId(_buildingSite.Id);
            _notifier = actorSpawner;
            _notifier.Spawned += OnSetActor;
        }

        public string CurrentActorId => _currentActor.Data.Id;

        public int BuildingSiteId => _buildingSite.Id;

        public int NumberLine => _buildingSite.NumberLine;

        public bool HasBuilding => _destroyableEvents != null;

        public bool IsConstruction => _notifier.IsInProcessSpawn;

        public void Disable()
        {
            _notifier.Spawned -= OnSetActor;

            if (_destroyableEvents != null)
                _destroyableEvents.Destroyed -= OnActivateBuildingSite;
        }

        public bool TryGetProductionStorage(out IProductionStorage productionStorage)
        {
            if (_currentActor.TryGetComponent(out IProductionService productionService))
            {
                productionStorage = productionService;

                return true;
            }

            productionStorage = null;

            return false;
        }

        public void Select() =>
            _buildingSite.Select();

        public void Unselect() =>
            _buildingSite.Unselect();

        private void OnSetActor(IActor actor)
        {
            if (actor == null) 
                throw new ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IProductionService productionService))
                productionService.SetBuildingSiteId(_buildingSite.Id);

            if (actor.TryGetComponent(out IDestroyComponent component))
            {
                _currentActor = actor;
                _destroyableEvents = component;
                _destroyableEvents.Destroyed += OnActivateBuildingSite;
                _buildingSite.Hide();

                ActorAdded?.Invoke(this);
                StateChanged?.Invoke();
            }
        }

        private void OnActivateBuildingSite()
        {
            _currentActor = _buildingSiteActor;
            _buildingSite.Show();
            _destroyableEvents.Destroyed -= OnActivateBuildingSite;
            _destroyableEvents = null;

            ActorMissing?.Invoke(this);
            StateChanged?.Invoke();
        }
    }
}