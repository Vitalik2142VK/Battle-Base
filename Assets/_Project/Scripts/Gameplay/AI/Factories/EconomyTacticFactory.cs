using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.AI.TacticTypes;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class EconomyTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesController _buildingSitesController;
        private readonly IMaterialRegistry _materialRegistry;

        public EconomyTacticFactory(
            IBuildingSitesController buildingSitesController, 
            IMaterialRegistry materialRegistry)
        {
            _buildingSitesController = buildingSitesController ?? throw new ArgumentNullException(nameof(buildingSitesController));
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
        }

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting.Type != TacticType.Economy || setting is IEconomyTacticSetting economyTacticSetting == false)
                return false;

            IMaterialData materialData = _materialRegistry.GetMaterialData(team);
            tactic = new EconomyTactic(_buildingSitesController, economyTacticSetting, materialData, team);

            return true;
        }
    }
}