using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.No
{
    public interface IRandomTacticSetting : ITacticSetting
    {
        public IEnumerable<string> ForbiddenActorIds { get; }

        public int MaxNumSpawn { get; }

        public int MinNumSpawn { get; }
    }
}