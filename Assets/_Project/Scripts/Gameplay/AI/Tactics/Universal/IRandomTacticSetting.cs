using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    public interface IRandomTacticSetting : ITacticSetting
    {
        public IEnumerable<string> ForbiddenActorsIds { get; }

        public int MaxNumSpawn { get; }

        public int MinNumSpawn { get; }
    }
}