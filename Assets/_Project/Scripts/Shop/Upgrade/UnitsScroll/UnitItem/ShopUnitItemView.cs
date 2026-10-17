using System;
using BattleBase.Localization;
using BattleBase.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.ShopSystem
{
    public class ShopUnitItemView : ButtonClickHandler
    {
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unselectedColor;
        [SerializeField] private Image _preview;
        [SerializeField] private LocalizedText _unitName;

        private Action<ShopUnitItemView> _clicked;

        public ILanguageTextsSet UnitName { get; private set; }

        public IUpgradeStatsInfo PanelInfo { get; private set; }

        public IActorItemConfig Info { get; private set; }

        public void SetInfo(IActorItemConfig info, Sprite preview, Action<ShopUnitItemView> clicked)
        {
            _preview.sprite = preview;
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

        public void Select()
        {
            if (_buttonImage != null)
                _buttonImage.color = _selectedColor;
        }

        public void Unselect()
        {
            if (_buttonImage != null)
                _buttonImage.color = _unselectedColor;
        }
    }
}