using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.AI.Tactics;
using BattleBase.Gameplay.AI.Tactics.Attack;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.AI.Factories.Tactic
{
    public class SpawnUnitsTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly TacticTool _tool;
        private readonly Randomizer _randomizer;

        public SpawnUnitsTacticFactory(
            IBuildingSitesStorage buildingSitesController, 
            TacticTool tool, 
            Randomizer randomizer)
        {
            _buildingSitesStorage = buildingSitesController ?? throw new ArgumentNullException(nameof(buildingSitesController));
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
        }

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting is ISpawnUnitsTacticSetting spawnUnitsTacticSetting == false)
                return false;

            _tool.Init(team);
            IBuildingSitesController controller = _buildingSitesStorage.GetBuildingSitesController(team);
            tactic = new SpawnUnitsTactic(_tool, controller, spawnUnitsTacticSetting, _randomizer);

            return true;
        }
    }
}