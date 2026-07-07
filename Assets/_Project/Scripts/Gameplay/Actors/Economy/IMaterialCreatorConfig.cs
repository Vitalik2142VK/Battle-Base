using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IMaterialCreatorConfig
    {
        public IEnumerable<IMaterialByRank> AddedMaterialsByRank { get; }

        public float AccrualTime { get; }
    }
}