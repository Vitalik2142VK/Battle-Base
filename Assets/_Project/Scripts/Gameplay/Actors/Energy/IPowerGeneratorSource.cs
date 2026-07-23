using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IPowerGeneratorSource : IComponentSource
    {
        public IEnumerable<IPowerByRank> AddedPowerByRank { get; }
    }
}
