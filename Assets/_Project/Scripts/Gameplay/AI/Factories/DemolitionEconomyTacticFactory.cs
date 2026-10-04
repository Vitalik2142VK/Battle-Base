using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.AI.Tactics;
using BattleBase.Gameplay.AI.Tactics.Economy;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class DemolitionEconomyTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly IMaterialRegistry _materialRegistry;
        private readonly Randomizer _radomizer;

        public DemolitionEconomyTacticFactory(
            IBuildingSitesStorage buildingSitesController,
            IMaterialRegistry materialRegistry,
            Randomizer radomizer)
        {
            _buildingSitesStorage = buildingSitesController ?? throw new ArgumentNullException(nameof(buildingSitesController));
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
            _radomizer = radomizer ?? throw new ArgumentNullException(nameof(radomizer));
        }

        public TacticCategory Category => TacticCategory.Economy;

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting is IDemolitionEconomyTacticSetting economyTacticSetting == false)
                return false;

            IBuildingSitesController controller = _buildingSitesStorage.GetBuildingSitesController(team);
            IMaterialData materialData = _materialRegistry.GetMaterialData(team);
            tactic = new DemolitionEconomyTactic(controller, economyTacticSetting, materialData, _radomizer);

            return true;
        }
    }
}