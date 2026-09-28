using BattleBase.ShopSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class PlayerDamageUpgrader : IActorComponentUpgrader
    {
        private readonly DamageUpgrader _damageUpgrader;

        public PlayerDamageUpgrader(IActorsUpgradeModel actorsUpgradeModel, IUpgraderConfig config)
        {
            if (actorsUpgradeModel == null)
                throw new ArgumentNullException(nameof(actorsUpgradeModel));

            Dictionary<string, IUpgradeLevel> upgradeLevels = new();

            foreach (var info in actorsUpgradeModel.Infos)
            {
                IUpgradeInfo upgradeInfo = info.PanelInfo.DamageInfo;
                upgradeLevels.Add(info.Id, upgradeInfo);
            }

            _damageUpgrader = new DamageUpgrader(upgradeLevels, config, TeamType.Player);
        }

        public TeamType Team => _damageUpgrader.Team;

        public void UpgradeActorComponents(IActor actor) =>
            _damageUpgrader.UpgradeActorComponents(actor);
    }
}