using System;
using BattleBase.Localization;
using BattleBase.UI.Buttons;
using TMPro;
using UnityEngine;

namespace BattleBase.Shop
{
    public class ShopUpgradeButton : ButtonClickHandler
    {
        [SerializeField] private LocalizedText _name;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _level;

        private Action _clicked;

        public void SetInfo(UpgradeButtonInfo info)
        {
            _name.SetTexts(info.Name);
            _price.text = info.Price.ToString();
            _level.text = $"{info.CurrentLevel}/{info.MaximumLevel}";
            _clicked = info.Clicked;
        }

        protected override void OnClick()
        {
            base.OnClick();

            _clicked?.Invoke();
        }
    }
}