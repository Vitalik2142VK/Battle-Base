using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.AI.TacticTypes;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class RandomTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesController _buildingSitesController;

        public RandomTacticFactory(IBuildingSitesController buildingSitesController)
        {
            _buildingSitesController = buildingSitesController ?? throw new ArgumentNullException(nameof(buildingSitesController));
        }

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting.Type != TacticType.Random || setting is IRandomTacticSetting randomTacticSetting == false)
                return false;

            tactic = new RandomTactic(_buildingSitesController, randomTacticSetting, team);

            return true;
        }
    }
}