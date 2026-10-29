using BattleBase.Gameplay.AI.Deactevators;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Factories.Deactevator
{
    public interface ITacticDeactivatorFactory
    {
        public ITacticDeactivator Create(IEnumerable<ITacticDeactivatorScore> scores);
    }
}