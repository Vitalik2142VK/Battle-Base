using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics
{
    public interface ITacticsFactory
    {
        public IEnumerable<ITactic> Create(IBrainConfig cofing);
    }
}