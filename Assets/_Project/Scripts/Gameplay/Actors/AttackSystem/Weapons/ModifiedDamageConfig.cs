using BattleBase.Gameplay.Actors.DamageSystem;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem.Weapons
{
    public class ModifiedDamageConfig : IDamageConfig
    {
        private readonly IDamageConfig _defaultConfig;

        public ModifiedDamageConfig(IDamageConfig defaultConfig)
        {
            _defaultConfig = defaultConfig ?? throw new ArgumentNullException(nameof(defaultConfig));
        }

        public ITargetingProfile TargetingProfile => _defaultConfig.TargetingProfile;

        public DamageMask DamageMask => _defaultConfig.DamageMask;

        public float Damage { get; private set; }

        public void Modify(float damageCoefficient)
        {
            if (damageCoefficient < 1f)
                throw new ArgumentOutOfRangeException(nameof(damageCoefficient));

            Damage = _defaultConfig.Damage * damageCoefficient;
        }

        public void Reset()
        {
            Damage = _defaultConfig.Damage;
        }
    }
}