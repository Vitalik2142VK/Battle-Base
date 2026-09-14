using BattleBase.Gameplay.Actors.Visual.Particle;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    [RequireComponent(typeof(ParticleProjectile))]
    public class ActorDestroyParticle : MonoBehaviour, IHealthViewComponent
    {
        private IHealthEvents _healthEvents;
        private ParticleProjectile _particle;

        private void Awake()
        {
            _particle = GetComponent<ParticleProjectile>();
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
            _particle.Play();
    }
}