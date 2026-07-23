using System;
using BattleBase.DI;
using BattleBase.UI.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ShopUpgradeButton : ButtonClickHandler, IInjectable
    {
        [SerializeField] private RectTransform _toRebuildLayout;
        [SerializeField] private Sprite _canPaySprite;
        [SerializeField] private Sprite _canNotPaySprite;
        [SerializeField] private Image _coin;
        [SerializeField] private Image _arrow;
        [SerializeField] private TMP_Text _fullStack;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Color _canPayColor;
        [SerializeField] private Color _canNotPayColor;

        private CreditsModel _credits;

        [Inject]
        public void Construct(CreditsModel credits) =>
            _credits = credits ?? throw new ArgumentNullException(nameof(credits));

        public void UpdateInfo(IUpgradeInfo info)
        {
            _price.text = info.CurrentPrice.ToString();

            bool isFullStack = info.CurrentLevel >= info.MaximumLevel;
            _coin.gameObject.SetActive(isFullStack == false);
            _price.gameObject.SetActive(isFullStack == false);
            _arrow.gameObject.SetActive(isFullStack == false);
            _fullStack.gameObject.SetActive(isFullStack);

            _level.text = $"{info.CurrentLevel}/{info.MaximumLevel}";

            bool canPay = _credits.Value >= info.CurrentPrice;
            _price.color = canPay ? _canPayColor : _canNotPayColor;
            _coin.sprite = canPay ? _canPaySprite : _canNotPaySprite;

            LayoutRebuilder.ForceRebuildLayoutImmediate(_toRebuildLayout);
        }
    }
}