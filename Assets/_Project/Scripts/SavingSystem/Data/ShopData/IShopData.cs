using System.Collections.Generic;

namespace BattleBase.SaveService
{
    public interface IShopData : IChangeTrackable<IShopData>
    {
        public int Credits { get; }

        public IReadOnlyList<IUnitUpgradeData> UnitsUpgrades { get; }
    }
}