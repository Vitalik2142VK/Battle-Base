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

            _notifier = actorSpawner;
            _notifier.Spawned += OnSetActor;
        }

        public string CurrentId => _currentActor.Data.Id;

        public int NumberLine => _buildingSite.NumberLine;

        public bool HasBuilding => _destroyableEvents != null;

        public bool IsConstruction => _notifier.IsInProcessSpawn;

        public void Disable()
        {
            _notifier.Spawned -= OnSetActor;

            if (_destroyableEvents != null)
                _destroyableEvents.Destroyed -= OnShowBuildingSite;
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

        private void OnSetActor(IActor actor)
        {
            if (actor == null) 
                throw new ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IDestroyComponent component))
            {
                _currentActor = actor;
                _destroyableEvents = component;
                _destroyableEvents.Destroyed += OnShowBuildingSite;
                _buildingSite.Hide();

                ActorAdded?.Invoke(this);
                StateChanged?.Invoke();
            }
        }

        private void OnShowBuildingSite()
        {
            _currentActor = _buildingSiteActor;
            _buildingSite.Show();
            _destroyableEvents.Destroyed -= OnShowBuildingSite;
            _destroyableEvents = null;

            ActorMissing?.Invoke(this);
            StateChanged?.Invoke();
        }
    }
}