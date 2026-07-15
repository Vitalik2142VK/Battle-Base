using System;
using BattleBase.UI.Buttons;
using BattleBase.UI.PopUps;
using UnityEngine;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ShopItemInfoPopUpBridge : MonoBehaviour
    {
        [SerializeField] private ItemInfoPopUp _popUp;
        [SerializeField] private ButtonClickHandler _itemInfoOpenerButton;

        private ActorsUpgradeModel _unitsUpgradeModel;
        private PreviewCreator _previewCreator;

        [Inject]
        public void Construct(ActorsUpgradeModel unitsUpgradeModel, PreviewCreator previewCreator)
        {
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
            _previewCreator = previewCreator ?? throw new ArgumentNullException(nameof(previewCreator));
        }

        private void OnEnable() =>
            _itemInfoOpenerButton.Clicked += OnClick;

        private void OnDisable() =>
            _itemInfoOpenerButton.Clicked -= OnClick;

        private void OnClick(ButtonClickHandler _)
        {
            IShopActorItemConfig selected = _unitsUpgradeModel.Selected;

            int squareTextureSize = 512;
            Sprite preview = _previewCreator.Create(selected.CleanPrefab, selected.PreviewScreenScale, squareTextureSize);

            ItemPopUpInfo info = new(
                preview, 
                selected.UnitName, 
                selected.Description);

            _popUp.SetInfo(info);
        }
    }
}