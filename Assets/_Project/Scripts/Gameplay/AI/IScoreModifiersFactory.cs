using BattleBase.Gameplay.AI.Modifiers;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public interface IScoreModifiersFactory
    {
        public IEnumerable<IAdvancedScoreModifier> Create(IBrainConfing cofing);
    }
}