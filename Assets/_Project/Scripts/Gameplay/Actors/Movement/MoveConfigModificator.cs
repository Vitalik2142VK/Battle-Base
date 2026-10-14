using BattleBase.Gameplay.Actors.ComponentImprovement;
using System;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class MoveConfigModificator
    {
        private readonly IUpgraderConfig _config;
        private readonly IUpgradeLevel _upgradeLevel;

        public MoveConfigModificator(IUpgraderConfig config, IUpgradeLevel upgradeLevel)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _upgradeLevel = upgradeLevel ?? throw new ArgumentNullException(nameof(upgradeLevel));
        }

        public float SpeedCoefficient => _config.SpeedCoefficientByLevel * _upgradeLevel.CurrentLevel + 1f;
    }
}
