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
        private IProductionOption _productionOption;

        public event Action<IProductionData> ItemClicked;

        public IProductionData Info => _productionOption.Data;

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

        public void SetInfo(IProductionOption productionOption)
        {
            _productionOption = productionOption;

            _icon.sprite = Info.Icon;
            _price.text = $"+{Info.Price}";
        }

        private void OnItemButton(ButtonClickHandler handler) =>
            ItemClicked?.Invoke(Info);

        private void OnMoreInfoClicked(ButtonClickHandler handler)
        {
            IProductionData info = Info;
            ItemPopUpInfo adaptInfo = new(info.Icon, info.Name, Info.Description);
            _popUp.SetInfo(adaptInfo);
        }
    }
}