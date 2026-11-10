using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.AI.Tactics;
using BattleBase.Gameplay.AI.Tactics.Defense;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.AI.Factories.Tactic
{
    public class LineDefenseTacticFactory : ITacticFactory
    {
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly IMainAreaDefenseAI _mainAreaDefenseAI;
        private readonly TacticTool _tool;
        private readonly Randomizer _randomizer;

        public LineDefenseTacticFactory(
            IBuildingSitesStorage buildingSitesStorage,
            IMainAreaDefenseAI mainAreaDefenseAI,
            TacticTool tool, 
            Randomizer randomizer)
        {
            _buildingSitesStorage = buildingSitesStorage ?? throw new ArgumentNullException(nameof(buildingSitesStorage));
            _mainAreaDefenseAI = mainAreaDefenseAI ?? throw new ArgumentNullException(nameof(mainAreaDefenseAI));
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
        }

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            tactic = null;

            if (setting is ILineDefenseTacticSetting lineDefenseSetting == false)
                return false;

            _tool.Init(team);

            IBuildingSitesController controller = _buildingSitesStorage
                .GetBuildingSitesController(team, SiteType.Defense);

            tactic = new LineDefenseTactic(_tool, controller, lineDefenseSetting, _mainAreaDefenseAI, _randomizer);

            return true;
        }
    }
}