using System.Collections.Generic;

namespace BattleBase.ShopSystem
{
    public interface IActorsUpgradeModel
    {
        public IReadOnlyList<IShopActorItemConfig> Infos { get; }
    }
}