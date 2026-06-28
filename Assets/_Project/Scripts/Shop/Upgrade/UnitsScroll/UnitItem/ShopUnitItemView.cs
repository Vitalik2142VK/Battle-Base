using System;
using BattleBase.Localization;
using BattleBase.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.ShopSystem
{
    public partial class ShopUnitItemView : ButtonClickHandler
    {
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unselectedColor;
        [SerializeField] private Image _preview;
        [SerializeField] private LocalizedText _unitName;

        private Action<ShopUnitItemView> _clicked;

        public ILanguageTextsSet UnitName { get; private set; }

        public IShopUpgradeStatsInfo PanelInfo { get; private set; }

        public IShopUnitItemInfo Info { get; private set; }

        public void SetInfo(IShopUnitItemInfo info, Action<ShopUnitItemView> clicked)
        {
            _preview.sprite = info.Preview;
            UnitName = info.UnitName;
            _unitName.SetTexts(UnitName);
            _clicked = clicked;
            PanelInfo = info.PanelInfo;
            Info = info;
        }

        protected override void OnClick()
        {
            base.OnClick();

            _clicked?.Invoke(this);
        }

        public void Select() =>
            _buttonImage.color = _selectedColor;

        public void Unselect() =>
            _buttonImage.color = _unselectedColor;
    }
}