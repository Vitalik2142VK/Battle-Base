using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public class ConsoleHealthCounter : MonoBehaviour, IHealthViewComponent
    {
        private IHealthEvents _healthEvents;

        private void OnEnable()
        {
            if (_healthEvents != null)
                _healthEvents.HealthChanged += OnUpdateDataHealth;
        }

        private void OnDisable()
        {
            if (_healthEvents != null)
                _healthEvents.HealthChanged -= OnUpdateDataHealth;
        }

        public void Init(IHealthEvents healthEvents)
        {
            _healthEvents = healthEvents ?? throw new ArgumentNullException(nameof(healthEvents));

            if (gameObject.activeSelf)
                _healthEvents.HealthChanged += OnUpdateDataHealth;
        }

        private void OnUpdateDataHealth(float maxHealth, float currentHealth)
        {
            if (maxHealth <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxHealth));

            if (currentHealth > maxHealth || currentHealth < 0)
                throw new ArgumentOutOfRangeException(nameof(currentHealth));

            Debug.Log($"Health = {maxHealth}/{currentHealth}");
        }
    }
}