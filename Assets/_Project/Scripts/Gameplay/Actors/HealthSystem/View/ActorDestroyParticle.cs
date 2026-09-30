using BattleBase.Gameplay.Actors.Visual.Particle;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.HealthSystem.View
{
    [RequireComponent(typeof(ParticleActivator))]
    public class ActorDestroyParticle : MonoBehaviour, IHealthViewComponent
    {
        private IHealthEvents _healthEvents;
        private ParticleActivator _activator;

        private void Awake()
        {
            _activator = GetComponent<ParticleActivator>();
        }

        private void OnEnable()
        {
            if (_healthEvents != null)
                _healthEvents.Destroyed += OnPlay;
        }

        private void OnDisable()
        {
            if (_healthEvents != null)
                _healthEvents.Destroyed -= OnPlay;
        }

        public void Init(IHealthEvents healthEvents)
        {
            _healthEvents = healthEvents ?? throw new ArgumentNullException(nameof(healthEvents));

            if (gameObject.activeSelf)
                _healthEvents.Destroyed += OnPlay;
        }

        private void OnPlay() =>
            _activator.Activate();
    }
}