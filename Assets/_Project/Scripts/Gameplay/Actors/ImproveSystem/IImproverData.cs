using BattleBase.Gameplay.Actors.Production;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImproverData : IProductionData
    {
        public  IEnumerable<int> ImprovePrices { get; }
    }
}