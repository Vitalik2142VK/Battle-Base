using BattleBase.Gameplay.Actors.HealthSystem;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    [RequireComponent(typeof(BoxCollider))]
    public class Target : MonoBehaviour, ITarget
    {
        private IHealthPresenter _healthPresenter;
        private IDamagebleEvents _damagebleEvents;
        private Transform _transform;
        private BoxCollider _collider;

        public event Action Destroyed;

        private void Awake()
        {
            _transform = transform;
            _collider = GetComponent<BoxCollider>();
        }

        private void OnEnable()
        {
            if (_healthPresenter != null)
                _damagebleEvents.Destroyed += OnDied;
        }

        private void OnDisable()
        {
            if (_healthPresenter != null)
                _damagebleEvents.Destroyed -= OnDied;
        }

        public void Init(IHealthPresenter healthPresenter, IDamagebleEvents damagebleEvents)
        {
            _healthPresenter = healthPresenter ?? throw new ArgumentNullException(nameof(healthPresenter));
            _damagebleEvents = damagebleEvents ?? throw new ArgumentNullException(nameof(damagebleEvents));

            if (gameObject.activeSelf)
                _damagebleEvents.Destroyed += OnDied;
        }

        public Vector3 Position => _transform.position;

        public bool HasHit(Vector3 hitPosition) => _collider.bounds.Contains(hitPosition);

        public void TakeDamage(IDamage damage)
        {
            _healthPresenter.SendDamage(damage);
        }

        public void OnDied()
        {
            Destroyed?.Invoke();
        }
    }
}