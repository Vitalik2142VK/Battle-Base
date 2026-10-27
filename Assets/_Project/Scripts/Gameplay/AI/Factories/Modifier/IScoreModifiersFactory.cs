using BattleBase.Gameplay.AI.Modifiers;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Factories.Modifier
{
    public interface IScoreModifiersFactory
    {
        public IEnumerable<IAdvancedScoreModifier> Create(IBrainConfig cofing);
    }
}