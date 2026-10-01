using BattleBase.Gameplay.Actors.Visual.Particle;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.HealthSystem.View
{
    [RequireComponent(typeof(ParticleActivator))]
    public class ActorDestroyParticle : MonoBehaviour, IHealthViewComponent
    {
        [SerializeField] private ParticleView _prefab;
        [SerializeField] private float _size = 1f;

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
            _healthEvents = healthEvents ?? throw new System.ArgumentNullException(nameof(healthEvents));

            if (gameObject.activeSelf)
                _healthEvents.Destroyed += OnPlay;
        }

        private void OnPlay() =>
            _activator.Activate(_prefab.Id, _size);
    }
}