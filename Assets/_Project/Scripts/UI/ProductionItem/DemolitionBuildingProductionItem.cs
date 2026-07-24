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
    public class DemolitionBuildingProductionItem : ProductionItemBase, IProductionItem, IInjectable
    {
        [SerializeField] private Image _icon;
        [SerializeField] private ButtonClickHandler _itemButton;
        [SerializeField] private ButtonClickHandler _moreInfoButton;
        [SerializeField] private TMP_Text _price;

        private ItemInfoPopUp _popUp;
        private IProductionOptionPresenter _presenter;
        private IProductionData _data;

        public event Action<IProductionData> ItemClicked;

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
        }

        private void OnDisable()
        {
            _itemButton.Clicked -= OnItemButton;
            _moreInfoButton.Clicked -= OnMoreInfoClicked;
        }

        public void SetParent(Transform parent) =>
            transform.SetParent(parent, false);

        public void ResetParent() =>
            transform.SetParent(null, false);

        public void Init(IProductionOptionPresenter presenter, IProductionData data)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _data = data ?? throw new ArgumentNullException(nameof(data));

            _icon.sprite = _data.Icon;
            _price.text = $"+{_data.Price}";
        }

        private void OnItemButton(ButtonClickHandler handler)
        {
            _presenter.HandleSelectButton();

            ItemClicked?.Invoke(_data);
        }

        private void OnMoreInfoClicked(ButtonClickHandler handler)
        {
            IProductionData info = _data;
            ItemPopUpInfo adaptInfo = new(info.Icon, info.Name, _data.Description);
            _popUp.SetInfo(adaptInfo);
        }
    }
}