using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.AI.TacticTypes;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class RandomTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;

        public RandomTacticFactory(IBuildingSitesStorage buildingSitesController)
        {
            _buildingSitesStorage = buildingSitesController ?? throw new ArgumentNullException(nameof(buildingSitesController));
        }

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting is IRandomTacticSetting randomTacticSetting == false)
                return false;

            IBuildingSitesController controller = _buildingSitesStorage.GetBuildingSitesController(team);
            tactic = new RandomTactic(controller, randomTacticSetting);

            return true;
        }
    }
}