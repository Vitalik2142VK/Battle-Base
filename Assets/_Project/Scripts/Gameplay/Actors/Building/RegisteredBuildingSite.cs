using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.Building
{
    public class RegisteredBuildingSite : IRegisteredBuildingSite
    {
        private readonly IActor _buildingSiteActor;
        private readonly IBuildingSite _buildingSite;
        private readonly IActorSpawnerEvents _events;

        private IActor _currentActor;
        private IDestroyableEvents _destroyableEvents;

        public RegisteredBuildingSite(IActor buildingSiteActor, IBuildingSite buildingSite)
        {
            _buildingSiteActor = buildingSiteActor ?? throw new ArgumentNullException(nameof(buildingSiteActor));
            _buildingSite = buildingSite ?? throw new ArgumentNullException(nameof(buildingSite));
            _currentActor = _buildingSiteActor;

            if (_buildingSiteActor.TryGetComponent(out IActorSpawner actorSpawner) == false)
                throw new InvalidOperationException($"{nameof(buildingSiteActor)} don't constrain component {nameof(IActorSpawner)}");

            _events = actorSpawner;
            _events.Spawned += OnSetActor;
        }

        public void Disable()
        {
            _events.Spawned -= OnSetActor;

            if (_destroyableEvents != null)
                _destroyableEvents.Destroyed -= OnShowBuildingSite;
        }

        public bool TryGetActorSpawner(out IProductionService productionService) =>
            _currentActor.TryGetComponent(out productionService);

        private void OnSetActor(IActor actor)
        {
            if (actor == null) 
                throw new ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IHealth health))
            {
                _currentActor = actor;
                _destroyableEvents = health;
                _destroyableEvents.Destroyed += OnShowBuildingSite;
                _buildingSite.Hide();
            }
        }

        private void OnShowBuildingSite()
        {
            _currentActor = _buildingSiteActor;
            _buildingSite.Show();
            _destroyableEvents.Destroyed -= OnShowBuildingSite;
            _destroyableEvents = null;
        }
    }
}