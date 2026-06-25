using System;
using BattleBase.Localization;
using BattleBase.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.ShopSystem
{
    public partial class ShopUnitItem : ButtonClickHandler
    {
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unselectedColor;
        [SerializeField] private Image _preview;
        [SerializeField] private LocalizedText _unitName;

        private Action<ShopUnitItem> _clicked;

        public LanguageTextsSet UnitName { get; private set; }

        public ShopUpgradePanelInfo PanelInfo { get; private set; }

        public ShopUnitItemInfo Info { get; private set; }

        public void SetInfo(ShopUnitItemInfo info)
        {
            _preview.sprite = info.Preview;
            UnitName = info.UnitName;
            _unitName.SetTexts(UnitName);
            _clicked = info.Clicked;
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