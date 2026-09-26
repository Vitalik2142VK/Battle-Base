using System;
using System.Collections.Generic;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Availability;
using BattleBase.PreviewCreatingSystem;
using BattleBase.ShopSystem;
using BattleBase.Utils;
using UnityEngine;
using VContainer;

namespace BattleBase.UI.PopUps
{
    public class ShopPopUp : PopUp
    {
        [SerializeField] private ShopUnitsScroll[] _scrolls;

        private ActorsUpgradeModel _unitsUpgradeModel;
        private IPreviewCreator _previewCreator;
        private IPlayerAvailableActorProvider _availableActorProvider;

        [Inject]
        public void Construct(
            ActorsUpgradeModel unitsUpgradeModel,
            IPreviewCreator previewCreator,
            IPlayerAvailableActorProvider availableActorProvider)
        {
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
            _previewCreator = previewCreator ?? throw new ArgumentNullException(nameof(previewCreator));
            _availableActorProvider = availableActorProvider ?? throw new ArgumentNullException(nameof(availableActorProvider));
        }

        public override void Init()
        {
            base.Init();

            IReadOnlyList<IActorItemConfig> availableInfos = GetAvailableInfos();
            List<Sprite> previews = new();
            PreviewCreateConfig previewConfig = _unitsUpgradeModel.PreviewCreateConfig;

            foreach (IActorItemConfig info in availableInfos)
            {
                Sprite preview = _previewCreator.Create(
                    info.SourcePrefab,
                    info.PreviewScreenScale,
                    previewConfig.SmallTextureSize,
                    previewConfig.CameraOffset,
                    previewConfig.ModelRotation,
                    previewConfig.DepthBits,
                    previewConfig.AntiAliasingLevel);

                previews.Add(preview);
            }

            foreach(ShopUnitsScroll shopUnitsScroll in _scrolls)
                shopUnitsScroll.Init(availableInfos, previews);
        }

        private IReadOnlyList<IActorItemConfig> GetAvailableInfos()
        {
#if UNITY_EDITOR // todo remove on release
            if (DebugSettingMenuScene.IsShowAllUnits)
                return _unitsUpgradeModel.Infos;
#endif

            HashSet<string> availableIds = new();

            foreach (IActorConfig config in _availableActorProvider.ActualConfigs)
                availableIds.Add(config.Data.Id);

            List<IActorItemConfig> result = new();

            foreach (IActorItemConfig info in _unitsUpgradeModel.Infos)
            {
                if (availableIds.Contains(info.Id))
                    result.Add(info);
            }

            return result;
        }
    }
}