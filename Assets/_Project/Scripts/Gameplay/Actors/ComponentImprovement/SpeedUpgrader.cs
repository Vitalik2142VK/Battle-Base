using BattleBase.Gameplay.Actors.Movement;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class SpeedUpgrader : IActorComponentUpgrader
    {
        private readonly Dictionary<string, IUpgradeLevel> _upgradeLevels;
        private readonly IUpgraderConfig _config;

        public SpeedUpgrader(Dictionary<string, IUpgradeLevel> upgradeLevels, IUpgraderConfig config, TeamType team)
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

            if (actor.TryGetComponent(out IMover mover) == false)
                throw new InvalidOperationException($"Actor.Id = {actor.Data.Id} don't contains component {nameof(IMover)}");

#if UNITY_EDITOR
            if (DebugSettingGameScene.IsUnitsMaxLevel)
                level = new MaxUpgradeLevel(level);
#endif
            MoveConfigModificator modificator = new(_config, level);
            mover.Upgrade(modificator);
        }
    }
}