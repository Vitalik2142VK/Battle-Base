using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Defense
{
    public interface IDefenseTacticSetting : ITacticSetting
    {
        public IEnumerable<int> LineNumbersForBuild { get; }

        public IEnumerable<string> GetDefenseBuildingIds();

        public int ScoreForBuild { get; }
    }
}