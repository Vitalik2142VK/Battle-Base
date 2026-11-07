using BattleBase.Gameplay.Actors.DamageSystem;
using System;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public class Health : IHealth
    {
        private readonly ModifiedHealthConfig _config;
        private readonly IDamageModifier _damageModifier;
        private readonly ActorMask _type;

        private IEnalableActor _enalableActor;
        private float _currentHealth;
        private bool _isEnable;

        public event Action<float, float> HealthChanged;
        public event Action Destroyed;

        public Health(IHealthConfig config, IDamageModifier damageModifier, ActorMask type)
        {
            _damageModifier = damageModifier ?? throw new ArgumentNullException(nameof(damageModifier));
            _type = type;

            _config = new ModifiedHealthConfig(config);
        }

        public Type KeyType => typeof(IHealth);

        public ActorMask ActorMask => _type;

        public void Init(IEnalableActor enalableActor)
        {
            _enalableActor = enalableActor ?? throw new ArgumentNullException(nameof(enalableActor));
        }

        public void Enable()
        {
            _isEnable = true;
            _currentHealth = _config.MaxHealth;

            HealthChanged?.Invoke(_config.MaxHealth, _currentHealth);
        }

        public void Disable()
        {
            _isEnable = false;

            if (_enalableActor.IsEnabled)
                return;

            _config.Reset();
            _currentHealth = 0;
        }

        public void TakeDamage(IDamage damage)
        {
            if (_isEnable == false)
                return;

            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            float finalDamage = _damageModifier.CalculateDamage(damage, _config);

            if (finalDamage < 0)
                throw new InvalidOperationException($"{nameof(finalDamage)} cannot be less than 0");

            _currentHealth -= finalDamage;

            if (_currentHealth > 0)
            {
                HealthChanged?.Invoke(_config.MaxHealth, _currentHealth);
            }
            else
            {
                _currentHealth = 0;

                Destroyed?.Invoke();
            }
        }

        public void Upgrade(HealthConfigModificator modificator) =>
            _config.Modify(modificator);
    }
}