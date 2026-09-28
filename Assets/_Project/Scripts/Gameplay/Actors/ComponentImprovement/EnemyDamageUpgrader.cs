using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class EnemyDamageUpgrader : IActorComponentUpgrader
    {
        private readonly DamageUpgrader _damageUpgrader;

        public EnemyDamageUpgrader(IEnemyActorsUpgrade enemyActorsUpgrade, IUpgraderConfig config)
        {
            if (enemyActorsUpgrade == null)
                throw new ArgumentNullException(nameof(enemyActorsUpgrade));

            Dictionary<string, IUpgradeLevel> upgradeLevels = new();

            foreach (var stat in enemyActorsUpgrade.Stats)
                upgradeLevels.Add(stat.ActorId, stat.DamageInfo);

            _damageUpgrader = new DamageUpgrader(upgradeLevels, config, TeamType.Enemy);
        }

        public TeamType Team => _damageUpgrader.Team;

        public void UpgradeActorComponents(IActor actor) =>
            _damageUpgrader.UpgradeActorComponents(actor);
    }
}