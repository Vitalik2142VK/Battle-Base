using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    public interface IImproveTacticSetting : ITacticSetting
    {
        public IEnumerable<string> ImprovedActorsIds { get; }

        public int MaxLevelImproved { get; }
    }
}