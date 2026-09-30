using BattleBase.Gameplay.Actors.ComponentImprovement;
using System;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public class HealthConfigModificator
    {
        private readonly IUpgraderConfig _config;
        private readonly IUpgradeLevel _upgradeLevel;

        public HealthConfigModificator(IUpgraderConfig config, IUpgradeLevel upgradeLevel)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _upgradeLevel = upgradeLevel ?? throw new ArgumentNullException(nameof(upgradeLevel));
        }

        public float HealthCoefficient => _config.HealthCoefficientByLevel * _upgradeLevel.CurrentLevel + 1f;
    }
}