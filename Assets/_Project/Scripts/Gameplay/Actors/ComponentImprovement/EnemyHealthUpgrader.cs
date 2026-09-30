using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class EnemyHealthUpgrader : IActorComponentUpgrader
    {
        private readonly HealthUpgrader _healthUpgrader;

        public EnemyHealthUpgrader(IEnemyActorsUpgrade enemyActorsUpgrade, IUpgraderConfig config)
        {
            if (enemyActorsUpgrade == null)
                throw new ArgumentNullException(nameof(enemyActorsUpgrade));

            Dictionary<string, IUpgradeLevel> upgradeLevels = new();

            foreach (var stat in enemyActorsUpgrade.Stats)
                upgradeLevels.Add(stat.ActorId, stat.HealthInfo);

            _healthUpgrader = new HealthUpgrader(upgradeLevels, config, TeamType.Enemy);
        }

        public TeamType Team => _healthUpgrader.Team;

        public void UpgradeActorComponents(IActor actor) =>
            _healthUpgrader.UpgradeActorComponents(actor);
    }
}