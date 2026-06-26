using System;
using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class ShopUnitItemInfo : IShopUnitItemInfo
    {
        [SerializeField] private Sprite _preview;
        [SerializeField] private LanguageTextsSet _unitName;
        [SerializeField] private ShopUpgradePanelInfo _panelInfo;

        public ShopUnitItemInfo(IShopUnitItemInfo other)
        {
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

        public Sprite Preview => _preview;

        public ILanguageTextsSet UnitName => _unitName;

        public IShopUpgradePanelInfo PanelInfo => _panelInfo;
    }
}