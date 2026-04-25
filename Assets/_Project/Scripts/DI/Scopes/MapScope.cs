using BattleBase.Commands;
using BattleBase.Core;
using BattleBase.Gameplay.Map;
using BattleBase.Gameplay.Map.InputSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using YG;

namespace BattleBase.DI
{
    public class MapScope : LifetimeScope 
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraArea _cameraArea;
        [SerializeField] private TerritorySelectPopUp _territorySelectPopUpPrefab;
        [SerializeField] private LoadGameSceneCommand _gameSceneLoader;
        [SerializeField] private MouseInputConfig _mouseMapCameraConfig;
        [SerializeField] private TouchInputConfig _touchMapCameraConfig;
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private TerritoryPositionAnimationConfig _territoryPositionAnimationConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_camera);
            builder.RegisterComponent<ICameraArea>(_cameraArea);
            builder.RegisterComponent<ICameraConfig>(_cameraConfig);
            builder.RegisterComponent(_gameSceneLoader);

            builder.RegisterComponent(_territorySelectPopUpPrefab);
            builder.Register<IFactory<TerritorySelectPopUp>, TerritorySelectPopUpFactory>(Lifetime.Scoped);
            builder.Register<IPool<TerritorySelectPopUp>, Pool<TerritorySelectPopUp>>(Lifetime.Scoped);
            builder.Register<ITerritorySelector, TerritorySelector>(Lifetime.Scoped);
            builder.RegisterComponent(_territoryPositionAnimationConfig);
            builder.Register<ITerritoryElevator, TerritoryElevator>(Lifetime.Scoped);
            builder.Register<ITerritoryPopUpShower, TerritoryPopUpShower>(Lifetime.Scoped);
            builder.Register<ICameraTracker, CameraTracker>(Lifetime.Scoped);

            builder.Register<IFrustumProjectionService, FrustumProjectionService>(Lifetime.Scoped);
            builder.Register<ICameraAreaService, CameraAreaService>(Lifetime.Scoped);
            builder.Register<IUIPointerChecker, UIPointerChecker>(Lifetime.Scoped);
            builder.Register<ICameraSnapBack, CameraSnapBack>(Lifetime.Scoped);
            builder.Register<ICameraBoundsLimiter, CameraBoundsLimiter>(Lifetime.Scoped);
            builder.Register<IVerticalFactorCalculator, VerticalFactorCalculator>(Lifetime.Scoped);
            builder.Register<ICameraOrientationAdapter, MapSceneCameraOrientationAdapter>(Lifetime.Scoped);
            builder.Register<ICameraZoom, CameraZoom>(Lifetime.Scoped);
            builder.Register<ICameraDragger, CameraDragger>(Lifetime.Scoped);
            builder.Register<ICameraInputReader, CameraInputReader>(Lifetime.Scoped);

            if (YG2.envir.isDesktop)
            {
                builder.RegisterComponent(_mouseMapCameraConfig).AsImplementedInterfaces();
                builder.Register<IClickDetector, MouseClickDetector>(Lifetime.Scoped);
                builder.Register<IMouseDragHandler, MouseDragHandler>(Lifetime.Scoped);
                builder.Register<IDragHandler, CompositeMouseDragHandler>(Lifetime.Scoped);
                builder.Register<IKeyboardDragHandler, KeyboardDragHandler>(Lifetime.Scoped);
                builder.Register<IZoomHandler, MouseZoomHandler>(Lifetime.Scoped);
            }
            else
            {
                builder.RegisterComponent(_touchMapCameraConfig).AsImplementedInterfaces();
                builder.Register<IClickDetector, TouchClickDetector>(Lifetime.Scoped);
                builder.Register<IDragHandler, TouchDragHandler>(Lifetime.Scoped);
                builder.Register<IZoomHandler, TouchPinchHandler>(Lifetime.Scoped);
            }

            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<ITerritoryElevator>();
                container.Resolve<ITerritoryPopUpShower>();
            });
        }
    }
}