using BattleBase.Gameplay.Actors.ComponentImprovement;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem.Weapons
{
    public class WeaponConfigModificator
    {
        private readonly IUpgraderConfig _config;
        private readonly IUpgradeLevel _upgradeLevel;

        public WeaponConfigModificator(IUpgraderConfig config, IUpgradeLevel upgradeLevel)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _upgradeLevel = upgradeLevel ?? throw new ArgumentNullException(nameof(upgradeLevel));
        }

        public float DamageCoefficient => _config.DamageCoefficientByLevel * _upgradeLevel.CurrentLevel + 1f;
    }
}