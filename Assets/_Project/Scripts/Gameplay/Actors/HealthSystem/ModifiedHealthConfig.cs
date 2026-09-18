using System;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public class ModifiedHealthConfig : IHealthConfig
    {
        private readonly IHealthConfig _defaultConfig;

        public ModifiedHealthConfig(IHealthConfig config)
        {
            _defaultConfig = config ?? throw new ArgumentNullException(nameof(config));

            Reset();
        }

        public float ArmorCoefficient => _defaultConfig.ArmorCoefficient;

        public float MaxHealth {  get; private set; }

        public void Modify(HealthConfigModificator modificator)
        {
            if (modificator == null)
                throw new ArgumentNullException(nameof(modificator));

            MaxHealth = _defaultConfig.MaxHealth * modificator.HealthCoefficient;
        }

        public void Reset()
        {
            MaxHealth = _defaultConfig.MaxHealth;
        }
    }
}