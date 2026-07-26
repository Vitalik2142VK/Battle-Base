using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public interface ITacticsFactory
    {
        public IEnumerable<ITactic> Create(IBrainConfing cofing);
    }
}