using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IPowerGeneratorSource
    {
        public IEnumerable<IPowerByRank> AddedPowerByRank { get; }
    }
}
