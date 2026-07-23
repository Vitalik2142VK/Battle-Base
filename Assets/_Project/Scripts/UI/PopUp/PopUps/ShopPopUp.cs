using System;
using System.Collections.Generic;
using BattleBase.PreviewCreatingSystem;
using BattleBase.ShopSystem;
using UnityEngine;
using VContainer;

namespace BattleBase.UI.PopUps
{
    public class ShopPopUp : PopUp
    {
        [SerializeField] private ShopUnitsScroll[] _scrolls;

        private ActorsUpgradeModel _unitsUpgradeModel;
        private IPreviewCreator _previewCreator;

        [Inject]
        public void Construct(
            ActorsUpgradeModel unitsUpgradeModel,
            IPreviewCreator previewCreator)
        {
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
            _previewCreator = previewCreator ?? throw new ArgumentNullException(nameof(previewCreator));
        }

        public override void Init()
        {
            base.Init();

            List<Sprite> previews = new();
            PreviewCreateConfig previewConfig = _unitsUpgradeModel.PreviewCreateConfig;

            foreach (IShopActorItemConfig info in _unitsUpgradeModel.Infos)
            {
                Sprite preview = _previewCreator.Create(
                    info.CleanPrefab,
                    info.PreviewScreenScale,
                    previewConfig.SmallTextureSize,
                    previewConfig.CameraOffset,
                    previewConfig.ModelRotation,
                    previewConfig.DepthBits,
                    previewConfig.AntiAliasingLevel);

                previews.Add(preview);
            }

            foreach(ShopUnitsScroll shopUnitsScroll in _scrolls)
                shopUnitsScroll.Init(_unitsUpgradeModel.Infos, previews);
        }
    }
}