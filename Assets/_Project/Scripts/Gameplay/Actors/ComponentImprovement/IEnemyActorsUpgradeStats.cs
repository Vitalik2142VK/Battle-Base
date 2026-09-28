using BattleBase.ShopSystem;
using System;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class EnemyActorUpgradeStats
    {
        public EnemyActorUpgradeStats(
            string actorId, 
            IUpgradeStatsInfo info, 
            IUpgraderConfig upgraderConfig, 
            float enemyUpgradeCoefficient)
        {
            if (string.IsNullOrEmpty(actorId))
                throw new ArgumentException(nameof(actorId));

            if (info == null)
                throw new ArgumentNullException(nameof(info));

            if (upgraderConfig == null)
                throw new ArgumentNullException(nameof(upgraderConfig));

            ActorId = actorId;
            DamageInfo = new EnemyActorUpgradeLevel(
                info.DamageInfo, 
                upgraderConfig.DamageCoefficientByLevel, 
                enemyUpgradeCoefficient);
            HealthInfo = new EnemyActorUpgradeLevel(
                info.HealthInfo,
                upgraderConfig.HealthCoefficientByLevel,
                enemyUpgradeCoefficient);
        }

        public IUpgradeLevel DamageInfo { get; }

        public IUpgradeLevel HealthInfo { get; }

        public IUpgradeLevel SpeedInfo { get; }

        public string ActorId { get; }
    }
}