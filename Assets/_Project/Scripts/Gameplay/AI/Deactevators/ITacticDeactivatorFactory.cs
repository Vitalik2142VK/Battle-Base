using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Deactevators
{
    public interface ITacticDeactivatorFactory
    {
        public ITacticDeactivator Create(IEnumerable<ITacticDeactivatorScore> scores);
    }
}