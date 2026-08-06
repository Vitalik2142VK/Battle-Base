using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public interface IScoreModifiersFactory
    {
        public IEnumerable<IAdvancedScoreModifier> Create(IBrainConfing cofing);
    }
}