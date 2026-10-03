using BattleBase.Gameplay.AI.Deactevators;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics
{
    public interface ITacticSetting
    {
        public IEnumerable<ITacticDeactivatorScore> DeactivatorScores { get; }

        public TacticCategory Category { get; }

        public int MaxScore { get; }

        public int MinScore { get; }
    }
}