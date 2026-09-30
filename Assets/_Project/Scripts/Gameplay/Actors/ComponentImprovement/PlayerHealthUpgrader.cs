using BattleBase.ShopSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class PlayerHealthUpgrader : IActorComponentUpgrader
    {
        private readonly HealthUpgrader _healthUpgrader;

        public PlayerHealthUpgrader(IActorsUpgradeModel actorsUpgradeModel, IUpgraderConfig config)
        {
            if (actorsUpgradeModel == null)
                throw new ArgumentNullException(nameof(actorsUpgradeModel));

            Dictionary<string, IUpgradeLevel> upgradeLevels = new();

            foreach (var info in actorsUpgradeModel.Infos)
            {
                IUpgradeInfo upgradeInfo = info.PanelInfo.HealthInfo;
                upgradeLevels.Add(info.Id, upgradeInfo);
            }

            _healthUpgrader = new HealthUpgrader(upgradeLevels, config, TeamType.Player);
        }

        public TeamType Team => _healthUpgrader.Team;

        public void UpgradeActorComponents(IActor actor) =>
            _healthUpgrader.UpgradeActorComponents(actor);
    }
}