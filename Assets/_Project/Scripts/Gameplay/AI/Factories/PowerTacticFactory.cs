using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.AI.Tactics;
using BattleBase.Gameplay.AI.Tactics.Energy;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class PowerTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly IPowerRegistry _powerRegistry;
        private readonly TacticTool _tool;

        public PowerTacticFactory(
            IBuildingSitesStorage buildingSitesController,
            IPowerRegistry powerRegistry,
            TacticTool tool)
        {
            _buildingSitesStorage = buildingSitesController ?? throw new ArgumentNullException(nameof(buildingSitesController));
            _powerRegistry = powerRegistry ?? throw new ArgumentNullException(nameof(powerRegistry));
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
        }

        public TacticCategory Category => TacticCategory.Power;

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting is IPowerTacticSetting powerSetting == false)
                return false;

            _tool.Init(team);
            IBuildingSitesController controller = _buildingSitesStorage.GetBuildingSitesController(team);
            IPowerData powerData = _powerRegistry.GetPowerData(team);
            tactic = new PowerTactic(_tool, controller, powerSetting, powerData);

            return true;
        }
    }
}