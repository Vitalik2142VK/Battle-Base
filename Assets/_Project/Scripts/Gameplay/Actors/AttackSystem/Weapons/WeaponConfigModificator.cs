using BattleBase.Gameplay.Actors.ComponentImprovement;
using BattleBase.ShopSystem;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem.Weapons
{
    public class WeaponConfigModificator : IWeaponConfigModificator
    {
        private readonly IUpgraderConfig _config;
        private readonly IUpgradeInfo _upgradeInfo;

        public WeaponConfigModificator(IUpgraderConfig config, IUpgradeInfo upgradeInfo)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _upgradeInfo = upgradeInfo ?? throw new ArgumentNullException(nameof(upgradeInfo));
        }

        public float DamageCoefficient => _config.DamageCoefficientByLevel * _upgradeInfo.CurrentLevel + 1f;
    }
}