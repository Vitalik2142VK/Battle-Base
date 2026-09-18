using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.ShopSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class HealthUpgrader : IActorComponentUpgrader
    {
        private readonly Dictionary<string, IUpgradeInfo> _infos;
        private readonly IUpgraderConfig _config;

        public HealthUpgrader(IActorsUpgradeModel actorsUpgradeModel, IUpgraderConfig config/*, TeamType team*/) // todo add TeamType for IActorsUpgradeModel
        {
            if (actorsUpgradeModel == null)
                throw new ArgumentNullException(nameof(actorsUpgradeModel));

            _config = config ?? throw new ArgumentNullException(nameof(config));

            _infos = new Dictionary<string, IUpgradeInfo>();

            foreach (var info in actorsUpgradeModel.Infos)
            {
                IUpgradeInfo upgradeInfo = info.PanelInfo.HealthInfo;
                _infos.Add(info.Id, upgradeInfo);
            }

            Team = TeamType.Player;
        }

        public TeamType Team { get; }

        public void UpgradeActorComponents(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (_infos.TryGetValue(actor.Data.Id, out IUpgradeInfo info) == false)
                return;

            if (actor.TryGetComponent(out IHealth health) == false)
                throw new InvalidOperationException($"Actor.Id = {actor.Data.Id} don't contains component {nameof(IHealth)}");

            HealthConfigModificator modificator = new(_config, info);
            health.Upgrade(modificator);
        }
    }
}