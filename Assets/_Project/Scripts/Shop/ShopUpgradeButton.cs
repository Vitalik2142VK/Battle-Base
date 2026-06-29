using System;
using BattleBase.DI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ShopUpgradeButton : MonoBehaviour, IInjectable
    {
        [SerializeField] private Sprite _canPaySprite;
        [SerializeField] private Sprite _canNotPaySprite;
        [SerializeField] private Button _button;
        [SerializeField] private Image _coin;
        [SerializeField] private Image _arrow;
        [SerializeField] private TMP_Text _fullStack;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Color _canPayColor;
        [SerializeField] private Color _canNotPayColor;

        private Action<ShopUpgradeButton> _clicked;

        public UpgradeButtonInfo Info { get; private set; }

        private CreditsModel _credits;
        
        public int Price => Info.Levels[Info.CurrentLevel].Price;

        private bool CanPay => _credits.Value >= Price;

        private bool IsFullStack => Info.CurrentLevel >= Info.MaximumLevel;

        [Inject]
        public void Construct(CreditsModel credits)
        {
            _credits = credits ?? throw new ArgumentNullException(nameof(credits));
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            _credits.Changed += OnCreditsChanged;
            OnCreditsChanged();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _credits.Changed -= OnCreditsChanged;
        }

        public void SetInfo(UpgradeButtonInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));

            UpdateInfo();
        }

        public void UpdateInfo()
        {
            if (Info == null)
                return;

            _price.text = Price.ToString();
            _level.text = $"{Info.CurrentLevel}/{Info.MaximumLevel}";
            _clicked = Info.Clicked;

            bool isFullStack = IsFullStack;

            _coin.gameObject.SetActive(isFullStack == false);
            _price.gameObject.SetActive(isFullStack == false);
            _arrow.gameObject.SetActive(isFullStack == false);
            _fullStack.gameObject.SetActive(isFullStack);
            _price.color = CanPay ? _canPayColor : _canNotPayColor;
            _coin.sprite = CanPay ? _canPaySprite : _canNotPaySprite;
        }

        private void OnClick()
        {
            if (IsFullStack)
                return;

            if (CanPay)
                _clicked?.Invoke(this);
        }

        private void OnCreditsChanged() =>
            UpdateInfo();
    }
}