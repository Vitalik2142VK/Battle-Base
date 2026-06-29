using System.Collections.Generic;

namespace BattleBase.ShopSystem
{
    public interface IUpgradeButtonInfo
    {
        public IReadOnlyList<int> Levels { get; }

        public int MaximumLevel { get; }

        public int CurrentLevel { get; }

        public int CurrentPrice { get; }
    }
}