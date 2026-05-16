using System;
using BattleBase.Localization;
using BattleBase.UI.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.UI
{
    [RequireComponent(typeof(Button))]
    public class ProductionItem : ButtonClickHandler, IProductionItem
    {
        [SerializeField] private Image _icon;
        [SerializeField] private LocalizedText _name;
        [SerializeField] private TMP_Text _price;

        public event Action<ProductionItem> Clicked;

        public IProductionItemInfo Info { get; private set; }

        public void SetParent(Transform parent) =>
            transform.SetParent(parent, false);

        public void ResetParent() =>
            transform.SetParent(null, false);

        public void SetInfo(IProductionItemInfo info)
        {
            Info = info;

            _icon.sprite = Info.Sprite;
            _name.SetTexts(info.Name);
            _price.text = Info.Price.ToString();
        }

        protected override void OnClick()
        {
            base.OnClick();
            Clicked?.Invoke(this);
        }
    }
}