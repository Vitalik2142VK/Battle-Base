using BattleBase.Commands;
using BattleBase.Core;
using BattleBase.Gameplay.CameraNavigation;
using BattleBase.Gameplay.CameraNavigation.InputReader;
using BattleBase.Gameplay.Map;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using YG;

namespace BattleBase.DI
{
    public class MapScope : LifetimeScope
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraRig _cameraRig;
        [SerializeField] private CameraArea _cameraArea;
        [SerializeField] private TerritorySelectPopUp _territorySelectPopUpPrefab;
        [SerializeField] private CommandLoadGameScene _commandLoadGameScene;
        [SerializeField] private CommandRebuildLayout _commandRebuildLayout;
        [SerializeField] private MouseInputConfig _mouseMapCameraConfig;
        [SerializeField] private TouchInputConfig _touchMapCameraConfig;
        [SerializeField] private TerritoryPositionAnimationConfig _territoryPositionAnimationConfig;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            RegisterCommands();
            RegisterCameraSystem();
            RegisterTerritorySystem();
            RegisterInputReader();
        }

        private void RegisterCommands()
        {
            _builder.RegisterComponent(_commandLoadGameScene);
            _builder.RegisterComponent(_commandRebuildLayout);
        }

        private void RegisterCameraSystem()
        {
            _builder.RegisterInstance(_camera);
            _builder.RegisterComponent(_cameraRig);
            _builder.RegisterComponent<ICameraArea>(_cameraArea);
            _builder.RegisterInstance(_cameraArea.Config).AsImplementedInterfaces();

            _builder.Register<ICameraHandle, CameraHandle>(Lifetime.Singleton);
            _builder.Register<IFrustumProjectionService, FrustumProjectionService>(Lifetime.Singleton);
            _builder.Register<ICameraSnapBack, CameraSnapBack>(Lifetime.Singleton);
            _builder.Register<IUIPointerChecker, UIPointerChecker>(Lifetime.Singleton);
            _builder.Register<IVerticalFactorCalculator, VerticalFactorCalculator>(Lifetime.Singleton);
            _builder.Register<ICameraOrientationAdapter, MapSceneCameraOrientationAdapter>(Lifetime.Singleton);
            _builder.Register<IScreenSizeTracker, ScreenSizeTracker>(Lifetime.Singleton);
            _builder.Register<IScreenOrientationTracker, ScreenOrientationTracker>(Lifetime.Singleton);
            _builder.Register<ICameraZoom, CameraZoom>(Lifetime.Singleton);
            _builder.Register<ICameraDragger, CameraDragger>(Lifetime.Singleton);
            _builder.Register<IResistanceCalculator, ResistanceCalculator>(Lifetime.Singleton);
            _builder.Register<IDragApplier, DragApplier>(Lifetime.Singleton);
            _builder.Register<IInertiaSnapbackApplier, InertiaSnapbackApplier>(Lifetime.Singleton);
        }

        private void RegisterTerritorySystem()
        {
            _builder.RegisterInstance(_territorySelectPopUpPrefab);
            _builder.RegisterInstance(_territoryPositionAnimationConfig);

            _builder.Register<ITerritorySelector, TerritorySelector>(Lifetime.Singleton);
            _builder.Register<IPool<TerritorySelectPopUp>, StaticPool<TerritorySelectPopUp>>(Lifetime.Singleton);
            _builder.Register<IFactory<TerritorySelectPopUp>, TerritorySelectPopUpFactory>(Lifetime.Singleton);
            _builder.Register<TerritoryElevator>(Lifetime.Singleton);
            _builder.Register<TerritoryPopUpShower>(Lifetime.Singleton);

            _builder.RegisterBuildCallback(container =>
            {
                container.Resolve<TerritoryElevator>();
                container.Resolve<TerritoryPopUpShower>();
            });
        }

        private void RegisterInputReader()
        {
            _builder.Register<ICameraInputReader, CameraInputReader>(Lifetime.Singleton);

            if (YG2.envir.isDesktop)
            {
                _builder.RegisterInstance(_mouseMapCameraConfig).AsImplementedInterfaces();
                _builder.Register<IClickDetector, MouseClickDetector>(Lifetime.Singleton);
                _builder.Register<IMouseDragHandler, MouseDragHandler>(Lifetime.Singleton);
                _builder.Register<IDragHandler, CompositeMouseDragHandler>(Lifetime.Singleton);
                _builder.Register<IKeyboardDragHandler, KeyboardDragHandler>(Lifetime.Singleton);
                _builder.Register<IZoomHandler, MouseZoomHandler>(Lifetime.Singleton);
            }
            else
            {
                _builder.RegisterInstance(_touchMapCameraConfig).AsImplementedInterfaces();
                _builder.Register<IClickDetector, TouchClickDetector>(Lifetime.Singleton);
                _builder.Register<IDragHandler, TouchDragHandler>(Lifetime.Singleton);
                _builder.Register<IZoomHandler, TouchPinchHandler>(Lifetime.Singleton);
            }
        }
    }
}