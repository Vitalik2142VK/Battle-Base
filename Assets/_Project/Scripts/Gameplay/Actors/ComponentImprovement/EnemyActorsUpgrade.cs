using BattleBase.Gameplay.Map;
using BattleBase.ShopSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class EnemyActorsUpgrade : IEnemyActorsUpgrade
    {
        private readonly List<EnemyActorUpgradeStats> _stats;

        public EnemyActorsUpgrade(
            ActorsUpgradeConfig config, 
            ISelectedTerritory selectedTerritory, 
            IUpgraderConfig upgraderConfig)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (selectedTerritory == null)
                throw new ArgumentNullException(nameof(selectedTerritory));

            if (upgraderConfig == null)
                throw new ArgumentNullException(nameof(upgraderConfig));

            _stats = new List<EnemyActorUpgradeStats>(config.Infos.Count);

            float upgradeActrosCoefficient = selectedTerritory.SelectedTerritoryInfo.UpgradeActrosCoefficient;
            CalculateUpgradeStats(config.Infos, upgraderConfig, upgradeActrosCoefficient);
        }

        public IEnumerable<EnemyActorUpgradeStats> Stats => _stats;

        private void CalculateUpgradeStats(
            IEnumerable<IActorItemConfig> configs, 
            IUpgraderConfig upgraderConfig, 
            float upgradeActrosCoefficient)
        {
            foreach (var config in configs)
            {
                EnemyActorUpgradeStats enemyActorUpgradeStats = new(
                    config.Id, 
                    config.PanelInfo, 
                    upgraderConfig, 
                    upgradeActrosCoefficient);
                _stats.Add(enemyActorUpgradeStats);
            }
        }
    }
}