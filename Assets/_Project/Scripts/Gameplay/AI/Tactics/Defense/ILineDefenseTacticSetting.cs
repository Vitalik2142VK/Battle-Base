using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Defense
{
    public interface ILineDefenseTacticSetting : ITacticSetting
    {
        public IEnumerable<string> GetDefenseBuildingIds();

        public int ScoreForActorInArea { get; }

        public int LineDefanceNumber { get; }
    }
}