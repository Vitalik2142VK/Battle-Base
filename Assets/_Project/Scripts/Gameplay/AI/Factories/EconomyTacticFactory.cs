using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.AI.Tactics;
using BattleBase.Gameplay.AI.Tactics.Economy;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class EconomyTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly IMaterialRegistry _materialRegistry;
        private readonly TacticTool _tool;

        public EconomyTacticFactory(
            IBuildingSitesStorage buildingSitesController,
            IMaterialRegistry materialRegistry,
            TacticTool tool)
        {
            _buildingSitesStorage = buildingSitesController ?? throw new ArgumentNullException(nameof(buildingSitesController));
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
        }

        public TacticCategory Category => TacticCategory.Economy;

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting is IEconomyTacticSetting economyTacticSetting == false)
                return false;

            _tool.Init(team);
            IBuildingSitesController controller = _buildingSitesStorage.GetBuildingSitesController(team);
            IMaterialData materialData = _materialRegistry.GetMaterialData(team);
            tactic = new EconomyTactic(_tool, controller, economyTacticSetting, materialData);

            return true;
        }
    }
}