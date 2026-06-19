using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.Building
{
    public class RegisteredBuildingSites
    {
        private readonly IBuildingSite _buildingSite;
        private readonly IActorSpawnerEvents _events;

        private IDestroyableEvents _destroyableEvents;

        public RegisteredBuildingSites(IBuildingSite buildingSite, IActorSpawnerEvents events)
        {
            _buildingSite = buildingSite ?? throw new ArgumentNullException(nameof(buildingSite));
            _events = events ?? throw new ArgumentNullException(nameof(events));

            _events.Spawned += OnSetActor;
        }

        public void Disabele()
        {
            _events.Spawned -= OnSetActor;

            if (_destroyableEvents != null)
                _destroyableEvents.Destroyed -= OnShowBuildingSite;
        }

        private void OnSetActor(IActor actor)
        {
            if (actor == null) 
                throw new ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IHealth health))
            {
                _destroyableEvents = health;
                _destroyableEvents.Destroyed += OnShowBuildingSite;
                _buildingSite.Hide();
            }
        }

        private void OnShowBuildingSite()
        {
            _buildingSite.Show();
            _destroyableEvents.Destroyed -= OnShowBuildingSite;
            _destroyableEvents = null;
        }
    }
}