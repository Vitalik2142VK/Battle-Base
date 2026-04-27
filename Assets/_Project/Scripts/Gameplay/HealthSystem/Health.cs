using BattleBase.Gameplay.DamageSystem;
using System;

namespace BattleBase.Gameplay.HealthSystem
{
    public class Health : IHealth
    {
        private readonly IHealthConfig _config;
        private readonly IHealthBar _healthBar;
        private readonly IDamageModifier _damageModifier;

        private float _currentHealth;

        public Health(IHealthConfig config, IHealthBar healthBar, IDamageModifier damageModifier)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _healthBar = healthBar ?? throw new ArgumentNullException(nameof(healthBar));
            _damageModifier = damageModifier ?? throw new ArgumentNullException(nameof(damageModifier));
        }

        public bool IsAlive => _currentHealth > 0;

        public void Restore()
        {
            _currentHealth = _config.MaxHealth;
            _healthBar.SetMaxHealth(_currentHealth);
            _healthBar.SetActive(false);
        }

        public void TakeDamage(IDamage damage)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            if (_healthBar.IsActive == false)
                _healthBar.SetActive(true);

            float finalDamage = _damageModifier.CalculateDamage(damage, _config);

            if (finalDamage < 0)
                throw new InvalidOperationException($"{nameof(finalDamage)} cannot be less than 0");

            _currentHealth -= finalDamage;

            if (IsAlive)
            {
                _healthBar.UpdateDataHealth(_currentHealth);
            }
            else
            {
                _currentHealth = 0;
                _healthBar.SetActive(false);
            }
        }
    }
}