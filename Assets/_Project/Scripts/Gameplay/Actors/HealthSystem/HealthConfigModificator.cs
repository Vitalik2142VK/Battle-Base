using BattleBase.Gameplay.Actors.ComponentImprovement;
using BattleBase.ShopSystem;
using System;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public class HealthConfigModificator
    {
        private readonly IUpgraderConfig _config;
        private readonly IUpgradeInfo _upgradeInfo;

        public HealthConfigModificator(IUpgraderConfig config, IUpgradeInfo upgradeInfo)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _upgradeInfo = upgradeInfo ?? throw new ArgumentNullException(nameof(upgradeInfo));
        }

        public float HealthCoefficient => _config.HealtheCoefficientByLevel * _upgradeInfo.CurrentLevel + 1f;
    }
}