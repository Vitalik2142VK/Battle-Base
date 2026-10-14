using BattleBase.ShopSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class PlayerSpeedUpgrader : IActorComponentUpgrader
    {
        private readonly SpeedUpgrader _speedUpgrader;

        public PlayerSpeedUpgrader(IActorsUpgradeModel actorsUpgradeModel, IUpgraderConfig config)
        {
            if (actorsUpgradeModel == null)
                throw new ArgumentNullException(nameof(actorsUpgradeModel));

            Dictionary<string, IUpgradeLevel> upgradeLevels = new();

            foreach (var info in actorsUpgradeModel.Infos)
            {
                IUpgradeInfo upgradeInfo = info.PanelInfo.SpeedInfo;
                upgradeLevels.Add(info.Id, upgradeInfo);
            }

            _speedUpgrader = new SpeedUpgrader(upgradeLevels, config, TeamType.Player);
        }

        public TeamType Team => _speedUpgrader.Team;

        public void UpgradeActorComponents(IActor actor) =>
            _speedUpgrader.UpgradeActorComponents(actor);
    }
}