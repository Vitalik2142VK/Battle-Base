using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem.Weapons
{
    public class ModifiedWeaponConfig : IWeaponConfig
    {
        private readonly IWeaponConfig _defaultConfig;
        private readonly ModifiedDamageConfig _damageConfig;

        public ModifiedWeaponConfig(IWeaponConfig defaultConfig)
        {
            _defaultConfig = defaultConfig ?? throw new ArgumentNullException(nameof(defaultConfig));
            _damageConfig = new ModifiedDamageConfig(_defaultConfig.DamageConfig);

            Reset();
        }

        public IProjectileConfig ProjectileConfig => _defaultConfig.ProjectileConfig;

        public IDamageConfig DamageConfig => _damageConfig;

        public float RateShooting => _defaultConfig.RateShooting;

        public float SpeedReload => _defaultConfig.SpeedReload;

        public int NumberShells => _defaultConfig.NumberShells;

        public float Range => _defaultConfig.Range;

        public void Modify(IWeaponConfigModificator modificator)
        {
            if (modificator == null)
                throw new ArgumentNullException(nameof(modificator));

            _damageConfig.Modify(modificator.DamageCoefficient);
        }

        public void Reset()
        {
            _damageConfig.Reset();
        }
    }
}