using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.AI.Tactics;
using BattleBase.Gameplay.AI.Tactics.No;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class RandomTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly Randomizer _randomizer;

        public RandomTacticFactory(IBuildingSitesStorage buildingSitesController, Randomizer randomizer)
        {
            _buildingSitesStorage = buildingSitesController ?? throw new ArgumentNullException(nameof(buildingSitesController));
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
        }

        public TacticCategory Category => TacticCategory.No;

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting is IRandomTacticSetting randomTacticSetting == false)
                return false;

            IBuildingSitesController controller = _buildingSitesStorage.GetBuildingSitesController(team);
            tactic = new RandomTactic(controller, randomTacticSetting, _randomizer);

            return true;
        }
    }
}