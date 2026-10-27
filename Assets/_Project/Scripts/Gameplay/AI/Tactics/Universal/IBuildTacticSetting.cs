using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    public interface IBuildTacticSetting : ITacticSetting
    {
        public string BuildingId { get; }

        public IEnumerable<int> PriorityLinesNumbers { get; }

        public int ScoreForBuild { get; }

        public int MaxBuildings { get; }
    }
}