using System;
using BattleBase.Commands;
using BattleBase.DI;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Production.Spawn;
using BattleBase.UI.Buttons;
using BattleBase.UI.PopUps;
using BattleBase.Utils.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.UI
{
    public class ActorSpawnProductionItem : ProductionItemBase, IProductionItem, IInjectable
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fill;
        [SerializeField] private ButtonClickHandler _itemButton;
        [SerializeField] private ButtonClickHandler _moreInfoButton;
        [SerializeField] private ButtonClickHandler _decrementButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _quantity;

        private IProductionOptionPresenter _presenter;
        private ISpawnProductionData _spawnData;
        private IProductionData _info;
        private ItemInfoPopUp _popUp;

        public event Action<IProductionData> ItemClicked;
        public event Action<IProductionData> DecrementClicked;

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
            _decrementButton.Clicked += OnDecrementClicked;

            if (_spawnData != null)
            {
                _spawnData.DataChanged += OnUpdateData;
                OnUpdateData();
            }
        }

        private void OnDisable()
        {
            _itemButton.Clicked -= OnItemButton;
            _moreInfoButton.Clicked -= OnMoreInfoClicked;
            _decrementButton.Clicked -= OnDecrementClicked;

            if (_spawnData != null)
                _spawnData.DataChanged -= OnUpdateData;
        }

        public void SetParent(Transform parent) =>
            transform.SetParent(parent, false);

        public void ResetParent() =>
            transform.SetParent(null, false);

        public void Init(
            IProductionOptionPresenter presenter, 
            ISpawnProductionData spawnData, 
            IProductionData productionData)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _spawnData = spawnData ?? throw new ArgumentNullException(nameof(spawnData));
            _info = productionData ?? throw new ArgumentNullException(nameof(productionData));

            _icon.sprite = _info.Icon;
            _price.text = _info.Price.ToString();

            if (gameObject.activeSelf)
            {
                _spawnData.DataChanged += OnUpdateData;
                OnUpdateData();
            }
        }

        private void SetProgress01() => 
            _fill.fillAmount = _spawnData.ConstructionProgress;

        private void SetQuantity()
        {
            if (_spawnData.Count == 0)
                _decrementButton.Hide();
            else
                _decrementButton.Show();

            _quantity.text = _spawnData.Count.ToString();
        }

        private void OnUpdateData() //todo check subscriptions
        {
            SetProgress01();
            SetQuantity();
        }

        private void OnItemButton(ButtonClickHandler handler)
        {
            _presenter.HandleSelectButton();

            ItemClicked?.Invoke(_info);
        }

        private void OnDecrementClicked(ButtonClickHandler handler)
        {
            _presenter.HandleDecrementButton();

            DecrementClicked?.Invoke(_info);
        }

        private void OnMoreInfoClicked(ButtonClickHandler handler)
        {
            IProductionData info = _info;
            ItemPopUpInfo adaptInfo = new(info.Icon, info.Name, _info.Description);
            _popUp.SetInfo(adaptInfo);
        }
    }
}