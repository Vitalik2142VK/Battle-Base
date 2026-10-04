using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.AI.Tactics;
using BattleBase.Gameplay.AI.Tactics.Defense;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class DefenseTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly TacticTool _tool;
        private readonly Randomizer _randomizer;

        public DefenseTacticFactory(IBuildingSitesStorage buildingSitesStorage, TacticTool tool, Randomizer randomizer)
        {
            _buildingSitesStorage = buildingSitesStorage ?? throw new ArgumentNullException(nameof(buildingSitesStorage));
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
        }

        public TacticCategory Category => TacticCategory.Defense;

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting is IDefenseTacticSetting defenseSetting == false)
                return false;

            _tool.Init(team);

            IBuildingSitesController controller = _buildingSitesStorage
                .GetBuildingSitesController(team, SiteType.Defense);

            tactic = new DefenseTactic(_tool, controller, defenseSetting, _randomizer);

            return true;
        }
    }
}