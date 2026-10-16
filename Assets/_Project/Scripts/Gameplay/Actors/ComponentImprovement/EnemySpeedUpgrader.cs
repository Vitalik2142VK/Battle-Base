using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class EnemySpeedUpgrader : IActorComponentUpgrader
    {
        private readonly SpeedUpgrader _speedUpgrader;

        public EnemySpeedUpgrader(IEnemyActorsUpgrade enemyActorsUpgrade, IUpgraderConfig config)
        {
            if (enemyActorsUpgrade == null)
                throw new ArgumentNullException(nameof(enemyActorsUpgrade));

            Dictionary<string, IUpgradeLevel> upgradeLevels = new();

            foreach (var stat in enemyActorsUpgrade.Stats)
            {
                if (stat.SpeedInfo.MaximumLevel > 0) // todo this is a crutch
                    upgradeLevels.Add(stat.ActorId, stat.SpeedInfo);
            }

            _speedUpgrader = new SpeedUpgrader(upgradeLevels, config, TeamType.Enemy);
        }

        public TeamType Team => _speedUpgrader.Team;

        public void UpgradeActorComponents(IActor actor) =>
            _speedUpgrader.UpgradeActorComponents(actor);
    }
}