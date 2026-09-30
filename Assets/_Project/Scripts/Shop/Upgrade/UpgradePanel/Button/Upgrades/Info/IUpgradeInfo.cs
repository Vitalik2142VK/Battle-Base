using BattleBase.Gameplay.Actors.ComponentImprovement;
using System.Collections.Generic;

namespace BattleBase.ShopSystem
{
    public interface IUpgradeInfo : IUpgradeLevel
    {
        public IReadOnlyList<int> Levels { get; }

        public int CurrentPrice { get; }
    }
}