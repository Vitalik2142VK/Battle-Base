using BattleBase.Gameplay.Actors.AttackSystem;
using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
using BattleBase.ShopSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public partial class DamageUpgrader : IActorComponentUpgrader
    {
        private readonly Dictionary<string, IUpgradeInfo> _infos;
        private readonly IUpgraderConfig _config;

        public DamageUpgrader(IActorsUpgradeModel actorsUpgradeModel, IUpgraderConfig config/*, TeamType team*/) // todo add TeamType for IActorsUpgradeModel
        {
            if (actorsUpgradeModel == null)
                throw new ArgumentNullException(nameof(actorsUpgradeModel));

            _config = config ?? throw new ArgumentNullException(nameof(config));

            _infos = new Dictionary<string, IUpgradeInfo>();

            foreach (var info in actorsUpgradeModel.Infos)
            {
                IUpgradeInfo upgradeInfo = info.PanelInfo.DamageInfo;
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

            if (actor.TryGetComponent(out IAttacker attacker) == false)
                throw new InvalidOperationException($"Actor.Id = {actor.Data.Id} don't contains component {nameof(IAttacker)}");

            WeaponConfigModificator modificator = new(_config, info);
            attacker.Upgrade(modificator);
        }
    }
}