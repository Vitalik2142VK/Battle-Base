using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.AI.Tactics;
using BattleBase.Gameplay.AI.Tactics.Universal;
using System;

namespace BattleBase.Gameplay.AI.Factories.Tactic
{
    public class BuildTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly TacticTool _tool;

        public BuildTacticFactory(IBuildingSitesStorage buildingSitesController, TacticTool tool)
        {
            _buildingSitesStorage = buildingSitesController ?? throw new ArgumentNullException(nameof(buildingSitesController));
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
        }

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting is IBuildTacticSetting buildTacticSetting == false)
                return false;

            _tool.Init(team);
            IBuildingSitesController controller = _buildingSitesStorage.GetBuildingSitesController(team);
            tactic = new BuildTactic(_tool, controller, buildTacticSetting);

            return true;
        }
    }
}