using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.AI.Tactics;
using BattleBase.Gameplay.AI.Tactics.Defense;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class DefenseTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly TacticTool _tool;

        public DefenseTacticFactory(
            IBuildingSitesStorage buildingSitesStorage,
            TacticTool tool)
        {
            _buildingSitesStorage = buildingSitesStorage ?? throw new ArgumentNullException(nameof(buildingSitesStorage));
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
        }

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting is IDefenseTacticSetting defenseSetting == false)
                return false;

            _tool.Init(team);

            IBuildingSitesController controller = _buildingSitesStorage.GetBuildingSitesController(team);

            tactic = new DefenseTactic(_tool, controller, defenseSetting);

            return true;
        }
    }
}