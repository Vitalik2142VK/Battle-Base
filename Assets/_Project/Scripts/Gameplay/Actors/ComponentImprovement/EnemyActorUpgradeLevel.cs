using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class EnemyActorUpgradeLevel : IUpgradeLevel
    {
        private readonly IUpgradeLevel _upgradeLevel;

        public EnemyActorUpgradeLevel(
            IUpgradeLevel upgradeLevel, 
            float coefficientByLevel, 
            float enemyUpgradeCoefficient)
        {
            if (coefficientByLevel < 0)
                throw new ArgumentOutOfRangeException(nameof(coefficientByLevel));

            if (enemyUpgradeCoefficient < 0 || coefficientByLevel > 1)
                throw new ArgumentOutOfRangeException(nameof(enemyUpgradeCoefficient));

            _upgradeLevel = upgradeLevel ?? throw new ArgumentNullException(nameof(upgradeLevel));

            if (enemyUpgradeCoefficient == 0)
                CurrentLevel = 0;
            else if (enemyUpgradeCoefficient == 1)
                CurrentLevel = upgradeLevel.MaximumLevel;
            else
                CurrentLevel = CalculateCurrentLevel(
                enemyUpgradeCoefficient,
                coefficientByLevel,
                _upgradeLevel.MaximumLevel);
        }

        public int MaximumLevel => _upgradeLevel.MaximumLevel;

        public int CurrentLevel { get; }

        private int CalculateCurrentLevel(float progress, float step, int maximumLevel)
        {
            int level = Mathf.FloorToInt(progress / step);

            return Mathf.Clamp(level, 0, maximumLevel);
        }
    }
}