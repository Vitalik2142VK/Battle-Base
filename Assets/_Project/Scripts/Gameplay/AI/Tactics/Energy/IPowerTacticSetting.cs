using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Energy
{
    public interface IPowerTacticSetting : ITacticSetting
    {
        public string PowerStationId { get; }

        public IEnumerable<int> LineNumbersForBuild { get; }

        public int ScoreForBuildStation { get; }

        public int MaxStations { get; }
    }
}