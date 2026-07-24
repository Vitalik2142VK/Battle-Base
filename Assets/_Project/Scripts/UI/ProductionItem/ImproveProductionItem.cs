using BattleBase.Commands;
using BattleBase.DI;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.ImproveSystem;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.UI.Buttons;
using BattleBase.UI.PopUps;
using BattleBase.Utils.Constants;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.UI
{
    public class ImproveProductionItem : ProductionItemBase, IProductionItem, IInjectable
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Activator _itemButtonActivator;
        [SerializeField] private ButtonClickHandler _itemButton;
        [SerializeField] private ButtonClickHandler _moreInfoButton;
        [SerializeField] private TMP_Text _price;

        private ItemInfoPopUp _popUp;
        private IProductionOptionPresenter _presenter;
        private IProductionData _data;
        private IMaterialData _materialData;
        private IImproverState _improverState;

        public event Action<IProductionData> ItemClicked;

        [Inject]
        public void Construct(
            ItemInfoPopUp popUp, 
            [Key(VContainerKeys.CommandShowItemInfoPopUp)] CommandBase commandShowItemInfoPopUp)
        {
            _popUp = popUp != null ? popUp : throw new ArgumentNullException(nameof(popUp));
            _moreInfoButton.AddCommand(commandShowItemInfoPopUp);
        }

        private void OnEnable()
        {
            _itemButton.Clicked += OnItemButton;
            _moreInfoButton.Clicked += OnMoreInfoClicked;

            if (_materialData != null)
            {
                _materialData.DataChanged += OnUpdateData;

                OnUpdateData();
            }
        }

        private void OnDisable()
        {
            _itemButton.Clicked -= OnItemButton;
            _moreInfoButton.Clicked -= OnMoreInfoClicked;

            if (_materialData != null)
                _materialData.DataChanged -= OnUpdateData;
        }

        public void SetParent(Transform parent) =>
            transform.SetParent(parent, false);

        public void ResetParent() =>
            transform.SetParent(null, false);

        public void Init(
            IProductionOptionPresenter presenter,
            IProductionData data,
            IMaterialData materialData,
            IImproverState improverState)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _materialData = materialData ?? throw new ArgumentNullException(nameof(materialData));
            _improverState = improverState ?? throw new ArgumentNullException(nameof(improverState));

            _icon.sprite = _data.Icon;
            _price.text = _data.Price.ToString();

            if (gameObject.activeSelf)
            {
                _materialData.DataChanged += OnUpdateData;

                OnUpdateData();
            }
        }

        private void OnUpdateData() => 
            _itemButtonActivator.SetActive(_improverState.CanBuy);

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