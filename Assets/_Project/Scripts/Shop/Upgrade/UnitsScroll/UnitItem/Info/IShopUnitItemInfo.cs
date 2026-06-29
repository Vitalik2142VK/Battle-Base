using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    public interface IShopUnitItemInfo
    {
        public Sprite Preview { get; }

        public ILanguageTextsSet UnitName { get; }

        public IShopUpgradeStatsInfo PanelInfo { get; }
    }
}