using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.AI.TacticTypes;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class EconomyTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly IMaterialRegistry _materialRegistry;

        public EconomyTacticFactory(
            IBuildingSitesStorage buildingSitesController, 
            IMaterialRegistry materialRegistry)
        {
            _buildingSitesStorage = buildingSitesController ?? throw new ArgumentNullException(nameof(buildingSitesController));
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
        }

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting.Type != TacticType.Economy || setting is IEconomyTacticSetting economyTacticSetting == false)
                return false;

            IBuildingSitesController controller = _buildingSitesStorage.GetBuildingSitesController(team);
            IMaterialData materialData = _materialRegistry.GetMaterialData(team);
            tactic = new EconomyTactic(controller, economyTacticSetting, materialData);

            return true;
        }
    }
}