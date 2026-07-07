using System;
using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class ShopUnitItemInfo : IShopActorItemConfig
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _preview;
        [SerializeField] private LanguageTextsSet _unitName;
        [SerializeField] private ShopUpgradeStatsInfo _panelInfo;

        public ShopUnitItemInfo(IShopActorItemConfig other)
        {
            _id = other.Id;
            _preview = other.Preview;
            _unitName = new(other.UnitName);
            _panelInfo = new(other.PanelInfo);
        }

        public void IncreaseDamageLevel() =>
            _panelInfo.IncreaseDamageLevel();

        public void IncreaseArmorLevel() =>
            _panelInfo.IncreaseArmorLevel();

        public void IncreaseBuildTimeLevel() =>
            _panelInfo.IncreaseBuildTimeLevel();

        public void SetDamageLevel(int level) =>
            _panelInfo.SetDamageLevel(level);

        public void SetArmorLevel(int level) =>
            _panelInfo.SetArmorLevel(level);

        public void SetBuildTimeLevel(int level) =>
            _panelInfo.SetBuildTimeLevel(level);

        public string Id => _id;

        public Sprite Preview => _preview;

        public ILanguageTextsSet UnitName => _unitName;

        public IShopUpgradeStatsInfo PanelInfo => _panelInfo;
    }
}