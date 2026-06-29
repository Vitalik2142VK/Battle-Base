using System;
using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class ShopUnitItemInfo
    {
        [field: SerializeField] public Sprite Preview;
        [field: SerializeField] public LanguageTextsSet UnitName;
        [field: SerializeField] public ShopUpgradePanelInfo PanelInfo;
        public Action<ShopUnitItem> Clicked;
    }
}