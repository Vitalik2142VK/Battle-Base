using BattleBase.Gameplay.Actors.AttackSystem;
using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class DamageUpgrader : IActorComponentUpgrader
    {
        private readonly Dictionary<string, IUpgradeLevel> _upgradeLevels;
        private readonly IUpgraderConfig _config;

        public DamageUpgrader(Dictionary<string, IUpgradeLevel> upgradeLevels, IUpgraderConfig config, TeamType team)
        {
            _upgradeLevels = upgradeLevels ?? throw new ArgumentNullException(nameof(upgradeLevels));
            _config = config ?? throw new ArgumentNullException(nameof(config));

            Team = team;
        }

        public TeamType Team { get; }

        public void UpgradeActorComponents(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (_upgradeLevels.TryGetValue(actor.Data.Id, out IUpgradeLevel level) == false)
                return;

            if (actor.TryGetComponent(out IAttacker attacker) == false)
                throw new InvalidOperationException($"Actor.Id = {actor.Data.Id} don't contains component {nameof(IAttacker)}");

            WeaponConfigModificator modificator = new(_config, level);
            attacker.Upgrade(modificator);
        }
    }
}