using System;
using BattleBase.Commands;
using BattleBase.DI;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.UI.Buttons;
using BattleBase.UI.PopUps;
using BattleBase.Utils.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.UI
{
    public class ProductionItem : MonoBehaviour, IProductionItem, IInjectable
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fill;
        [SerializeField] private ButtonClickHandler _itemButton;
        [SerializeField] private ButtonClickHandler _moreInfoButton;
        [SerializeField] private ButtonClickHandler _decrementButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _quantity;

        private ItemInfoPopUp _popUp;
        private ProductionOption _productionOption;

        public event Action<ProductionOption> ItemClicked;
        public event Action<ProductionOption> DecrementClicked;

        public IProductionData Info => _productionOption.ProductionData;

        [Inject]
        public void Construct(ItemInfoPopUp popUp, [Key(VContainerKeys.CommandShowItemInfoPopUp)] CommandBase commandShowItemInfoPopUp)
        {
            _popUp = popUp != null ? popUp : throw new ArgumentNullException(nameof(popUp));
            _moreInfoButton.AddCommand(commandShowItemInfoPopUp);
        }

        private void OnEnable()
        {
            _itemButton.Clicked += OnItemButton;
            _moreInfoButton.Clicked += OnMoreInfoClicked;
            _decrementButton.Clicked += OnDecrementClicked;
        }

        private void OnDisable()
        {
            _itemButton.Clicked -= OnItemButton;
            _moreInfoButton.Clicked -= OnMoreInfoClicked;
            _decrementButton.Clicked -= OnDecrementClicked;
        }

        public void SetParent(Transform parent) =>
            transform.SetParent(parent, false);

        public void ResetParent() =>
            transform.SetParent(null, false);

        public void SetInfo(ProductionOption productionOption)
        {
            _productionOption = productionOption;

            _icon.sprite = Info.Icon;
            _price.text = Info.Price.ToString();
        }

        public void SetProgress01(float progress)
        {
            progress = Mathf.Clamp01(progress);
            _fill.fillAmount = 1 - progress;
        }

        public void SetQuantity(int value)
        {
            if (value < 0)
                throw new IndexOutOfRangeException(nameof(value));

            if (value == 0)
                _decrementButton.Hide();
            else
                _decrementButton.Show();

            _quantity.text = value.ToString();
        }

        private void OnItemButton(ButtonClickHandler handler) =>
            ItemClicked?.Invoke(_productionOption);

        private void OnMoreInfoClicked(ButtonClickHandler handler)
        {
            IProductionData info = Info;
            ItemPopUpInfo adaptInfo = new(info.Icon, info.Name, Info.Description);
            _popUp.SetInfo(adaptInfo);
        }

        private void OnDecrementClicked(ButtonClickHandler handler) =>
            DecrementClicked?.Invoke(_productionOption);
    }
}