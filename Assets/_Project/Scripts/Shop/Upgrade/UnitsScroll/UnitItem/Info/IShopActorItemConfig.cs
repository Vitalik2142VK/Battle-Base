using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    public interface IShopActorItemConfig
    {
        public string Id { get; }

        public Sprite Preview { get; }

        public GameObject CleanPrefab { get; }

        public ILanguageTextsSet UnitName { get; }

        public IShopUpgradeStatsInfo PanelInfo { get; }
    }
}