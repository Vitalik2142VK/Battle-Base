using BattleBase.Commands;
using BattleBase.Core;
using BattleBase.Gameplay;
using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.Actors.Visual.Select;
using BattleBase.Gameplay.CameraNavigation;
using BattleBase.Gameplay.CameraNavigation.InputReader;
using BattleBase.Gameplay.MiniMap;
using BattleBase.UI;
using BattleBase.UI.PopUps;
using BattleBase.Utils.Constants;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using YG;

namespace BattleBase.DI
{
    public class GameScopeFix : LifetimeScope
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraRig _cameraRig;
        [SerializeField] private CameraArea _cameraArea;
        [SerializeField] private MouseInputConfig _mouseMapCameraConfig;
        [SerializeField] private TouchInputConfig _touchMapCameraConfig;
        [SerializeField] private IconMapObject _iconMapObjectPrefab;
        [SerializeField] private ProductionItem _itemPrefab;
        [SerializeField] private ItemInfoPopUp _itemInfoPopUp;
        [SerializeField] private CommandBase _commandShowItemInfoPopUp;
        [SerializeField] private PowerConfig _powerConfig;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            RegisterMiniMapSystem();
            RegisterCameraSystem();
            RegisterCameraInputReader();
            RegisterBuildingSiteSelector();
            RegisterPowerRegistry();
        }

        private void RegisterBuildingSiteSelector()
        {
            _builder.Register<ISelector, Selector>(Lifetime.Singleton);
            _builder.Register<IProductionItemFactory, ProductionItemFactory>(Lifetime.Singleton);
            _builder.RegisterInstance(_itemPrefab);

            _builder.RegisterComponent(_itemInfoPopUp);
            _builder.RegisterInstance(_commandShowItemInfoPopUp).Keyed(VContainerKeys.CommandShowItemInfoPopUp);
        }

        private void RegisterMiniMapSystem()
        {
            _builder.Register<IEntityTrackersRegistry, EntityTrackersRegistry>(Lifetime.Singleton);
            _builder.Register<IPool<IconMapObject>, StaticPool<IconMapObject>>(Lifetime.Singleton);
            _builder.Register<IEntitySizeCalculator, WorldBoundsSizeCalculator>(Lifetime.Transient);
            _builder.Register<IEntityTrackerFactory, EntityTrackerFactory>(Lifetime.Singleton);
            _builder.Register<IFactory<IconMapObject>, IconMapObjectFactory>(Lifetime.Singleton);
            _builder.RegisterInstance(_iconMapObjectPrefab);
        }

        private void RegisterCameraSystem()
        {
            _builder.RegisterComponent(_camera);
            _builder.RegisterComponent(_cameraRig);
            _builder.RegisterComponent<ICameraArea>(_cameraArea);
            _builder.RegisterComponent(_cameraArea.Config).AsImplementedInterfaces();

            _builder.Register<ICameraHandle, CameraHandle>(Lifetime.Singleton);
            _builder.Register<IFrustumProjectionService, FrustumProjectionService>(Lifetime.Singleton);
            _builder.Register<ICameraSnapBack, CameraSnapBack>(Lifetime.Singleton);
            _builder.Register<IUIPointerChecker, UIPointerChecker>(Lifetime.Singleton);
            _builder.Register<IVerticalFactorCalculator, VerticalFactorCalculator>(Lifetime.Singleton);
            _builder.Register<ICameraOrientationAdapter, GameSceneCameraOrientationAdapter>(Lifetime.Singleton);
            _builder.Register<IScreenSizeTracker, ScreenSizeTracker>(Lifetime.Singleton);
            _builder.Register<IScreenOrientationTracker, ScreenOrientationTracker>(Lifetime.Singleton);
            _builder.Register<ICameraZoom, CameraZoom>(Lifetime.Singleton);
            _builder.Register<ICameraDragger, CameraDragger>(Lifetime.Singleton);
            _builder.Register<IResistanceCalculator, ResistanceCalculator>(Lifetime.Singleton);
            _builder.Register<IDragApplier, DragApplier>(Lifetime.Singleton);
            _builder.Register<IInertiaSnapbackApplier, InertiaSnapbackApplier>(Lifetime.Singleton);
        }

        private void RegisterCameraInputReader()
        {
            _builder.Register<ICameraInputReader, CameraInputReader>(Lifetime.Singleton);

            if (YG2.envir.isDesktop)
            {
                _builder.RegisterComponent(_mouseMapCameraConfig).AsImplementedInterfaces();
                _builder.Register<IClickDetector, MouseClickDetector>(Lifetime.Singleton);
                _builder.Register<IMouseDragHandler, MouseDragHandler>(Lifetime.Singleton);
                _builder.Register<IDragHandler, CompositeMouseDragHandler>(Lifetime.Singleton);
                _builder.Register<IKeyboardDragHandler, KeyboardDragHandler>(Lifetime.Singleton);
                _builder.Register<IZoomHandler, MouseZoomHandler>(Lifetime.Singleton);
            }
            else
            {
                _builder.RegisterComponent(_touchMapCameraConfig).AsImplementedInterfaces();
                _builder.Register<IClickDetector, TouchClickDetector>(Lifetime.Singleton);
                _builder.Register<IDragHandler, TouchDragHandler>(Lifetime.Singleton);
                _builder.Register<IZoomHandler, TouchPinchHandler>(Lifetime.Singleton);
            }
        }

        private void RegisterPowerRegistry()
        {
            _builder.RegisterInstance<IPowerConfig>(_powerConfig);
            _builder.Register<PowerRegistry>(Lifetime.Singleton)
                .As<IAdvancedPowerRegistry>()
                .As<IPowerRegistry>();
        }
    }
}